using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizAppApi.Models;
using QuizAppApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;


namespace QuizAppApi.Controllers
{
    [Route("quizapp/[controller]")]

    public class DeleteController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IChallengeService _challengeService;
        private readonly ICategoryService _categoryService;

        public DeleteController(IQuestionService questionService, IChallengeService challengeService, ICategoryService categoryService)
        {
            _questionService = questionService;
            _challengeService = challengeService;
            _categoryService = categoryService;
        }

        // DELETE api/quizapp/5
        [HttpDelete("questions/{id}")]
        public void DeleteQuestion(int id)
        {
            _questionService.Delete(id);
        }

        // DELETE api/quizapp/5
        [HttpDelete("categories/{id}")]
        public void DeleteCategory(int id)
        {
            _categoryService.Delete(id);
        }

        // DELETE api/quizapp/5
        [HttpDelete("challenges/{id}")]
        public void DeleteChallenge(int id)
        {
            _challengeService.Delete(id);
        }
    }
}
