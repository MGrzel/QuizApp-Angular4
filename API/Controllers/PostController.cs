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

    public class PostController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IChallengeService _challengeService;
        private readonly ICategoryService _categoryService;
        private readonly ISessionService _sessionService;

        public PostController(IQuestionService questionService, IChallengeService challengeService, ICategoryService categoryService, ISessionService sessionService)
        {
            _questionService = questionService;
            _challengeService = challengeService;
            _categoryService = categoryService;
            _sessionService = sessionService;
        }


        [HttpPost("challenges")]
        public object AddChallenge([FromBody]JObject challenge)
        {
            Challenge c = challenge.ToObject<Challenge>();
            if(_challengeService.CheckIfExists(c) || !_challengeService.Validate(c))
            {
                return BadRequest();
            }

            _challengeService.Add(c);
            return challenge;
        }

        // PUT api/quizapp
        [HttpPost("questions")]
        public object AddQuestion([FromBody]JObject question)
        {
            Question q = question.ToObject<Question>();
            if(_questionService.CheckIfExists(q) || !_questionService.Validate(q))
            {
                return BadRequest();
            }

            _questionService.Add(q);
            return question;
        }

        // PUT api/quizapp
        [HttpPost("categories")]
        public object AddCategory([FromBody]JObject category)
        {
            Category c = category.ToObject<Category>();
            if(_categoryService.CheckIfExists(c) || !_categoryService.Validate(c))
            {
                return BadRequest();
            }

            _categoryService.Add(c);
            return category;
        }

        // POST api/quizapp/{quiz}
        [HttpPost("sessions")]
        public object CreateSession([FromBody]JObject session)
        {
            Session quiz = session.ToObject<Session>();
            quiz = _sessionService.CheckQuizAnswers(quiz);
            _sessionService.SaveSession(quiz);
            return quiz;
        }
    }
}
