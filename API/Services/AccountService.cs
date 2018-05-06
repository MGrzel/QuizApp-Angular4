using Microsoft.AspNetCore.Identity;
using QuizAppApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizAppApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly QuizAppDb _context;
        private readonly UserManager<User> _userManager;

        public AccountService(QuizAppDb context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<User> GetUserFromJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            Claim claim = jwtToken.Claims.First(c => c.Type == "sub");
            string user = claim.Value;

            return await GetUserByNameAsync(user);
        }
    }
}
