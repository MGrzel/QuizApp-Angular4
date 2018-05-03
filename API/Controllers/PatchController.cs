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

    public class PatchController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IChallengeService _challengeService;
        private readonly ICategoryService _categoryService;

        public PatchController(IQuestionService questionService, IChallengeService challengeService, ICategoryService categoryService)
        {
            _questionService = questionService;
            _challengeService = challengeService;
            _categoryService = categoryService;
        }


        [HttpPatch("questions/{id}")]
        public void RestoreQuestion(int id, [FromBody]JObject question)
        {
            Question q = question.ToObject<Question>();
            _questionService.Restore(q);
        }


        [HttpPatch("categories/{id}")]
        public void RestoreCategory(int id, [FromBody]JObject category)
        {
            Category c = category.ToObject<Category>();
            _categoryService.Restore(c);
        }


        [HttpPatch("challenges/{id}")]
        public void RestoreChallenge(int id, [FromBody]JObject challenge)
        {
            Challenge c = challenge.ToObject<Challenge>();
            _challengeService.Restore(c);
        }
    }
}
