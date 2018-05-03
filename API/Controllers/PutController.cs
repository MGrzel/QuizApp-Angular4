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

    public class PutController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IChallengeService _challengeService;
        private readonly ICategoryService _categoryService;

        public PutController(IQuestionService questionService, IChallengeService challengeService, ICategoryService categoryService)
        {
            _questionService = questionService;
            _challengeService = challengeService;
            _categoryService = categoryService;
        }

        // PUT api/quizapp
        [HttpPut("questions/{id?}")]
        public object UpdateQuestion(int? id, [FromBody]JObject question)
        {
            if (id.HasValue)
            {
                Question q = question.ToObject<Question>();
                if (!_questionService.CheckIfExists(q) || !_questionService.Validate(q))
                {
                    return BadRequest();
                }

                _questionService.Update(q);
                return question;
            }
            return question;
        }

        // PUT api/quizapp
        [HttpPut("challenges/{id?}")]
        public object UpdateChallenge(int? id, [FromBody]JObject challenge)
        {
            if (id.HasValue)
            {
                Challenge c = challenge.ToObject<Challenge>();
                if (!_challengeService.CheckIfExists(c) || !_challengeService.Validate(c))
                {
                    return BadRequest();
                }

                _challengeService.Update(c);
                return challenge;
            }
            return challenge;
        }

        // PUT api/quizapp
        [HttpPut("categories/{id?}")]
        public object UpdateCategory(int? id, [FromBody]JObject category)
        {
            if (id.HasValue)
            {
                Category c = category.ToObject<Category>();
                if (!_categoryService.CheckIfExists(c) || !_categoryService.Validate(c))
                {
                    return BadRequest();
                }

                _categoryService.Update(c);
                return category;
            }
            return category;
        }
    }
}
