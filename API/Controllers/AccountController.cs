using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizAppApi.Models;
using QuizAppApi.Services;

namespace QuizAppApi.Controllers
{
    [Produces("application/json")]
    [Route("quizapp/[controller]")]
    public class AccountController : Controller
    {
        private readonly QuizAppDb _context;
        private readonly AnswerService _answerService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(QuizAppDb context, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _context = context;
            _answerService = new AnswerService(_context);
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("users/register")]
        public async Task<IActionResult> Register([FromBody]RegistrationUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User
            {
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return new OkObjectResult("Account created");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return new BadRequestObjectResult(ModelState);
            }
        }

        [HttpPost("users/token")]
        public async Task<IActionResult> GetToken([FromBody]LoginUser model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if(user == null)
                {
                    return new BadRequestObjectResult("User not found!");
                }

                var result = await _userManager.CheckPasswordAsync(user, model.Password);

                if(!result)
                {
                    return new BadRequestObjectResult("Password incorrect!");
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    notBefore: DateTime.Now,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256));

                return new OkObjectResult(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return new BadRequestObjectResult("Object not valid!");
        }
    }
}
