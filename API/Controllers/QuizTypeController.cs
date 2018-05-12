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
    public class QuizTypeController : Controller
    {
        private readonly IQuizTypeService _quizTypeService;

        public QuizTypeController(IQuizTypeService quizTypeService)
        {
            _quizTypeService = quizTypeService;
        }

        [HttpGet("get/quiztypes")]
        public async Task<object> GetQuizTypeList()
        {
            return await _quizTypeService.GetList();
        }
    }
}
