using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAppApi.Services;

namespace QuizAppApi.Controllers
{
    [Route("quizapp")]
    [Authorize("Bearer")]
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("get/colors/{name}")]
        public async Task<object> GetColorsByName(string name)
        {
            return await _colorService.GetByName(name);
        }

        [HttpGet("get/colors/{id?}")]
        public async Task<object> GetColors(Guid? id)
        {
            if (id.HasValue)
            {
                return await _colorService.GetById(id);
            }
            return await _colorService.GetList();
        }
    }
}
