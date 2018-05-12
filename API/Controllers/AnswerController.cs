using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAppApi.Models;
using QuizAppApi.Services;

namespace QuizAppApi.Controllers
{
    [Route("quizapp")]
    [Authorize("Bearer")]
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet("get/correctanswer/{questionId}/{answerId}")]
        public async Task<bool> CheckSingleAnswer(Guid questionId, Guid answerId)
        {
            return await _answerService.CheckAnswer(answerId);
        }

        [HttpGet("get/correctanswer/{id}")]
        public async Task<Answer> GetCorrectAnswer(Guid id)
        {
            return await _answerService.GetCorrectByQuestionId(id);
        }
    }
}
