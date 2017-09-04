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
        QuizAppDb QuizDb = new QuizAppDb();

        // PUT api/quizapp
        [HttpPost("challenges")]
        public object AddChallenge([FromBody]JObject challenge)
        {
            Challenge c = challenge.ToObject<Challenge>();
            if(ChallengeService.CheckIfExists(c) || !ChallengeService.Validate(c))
            {
                return BadRequest();
            }

            ChallengeService.Add(c);
            return challenge;
        }

        // PUT api/quizapp
        [HttpPost("questions")]
        public object AddQuestion([FromBody]JObject question)
        {
            Question q = question.ToObject<Question>();
            if(QuestionService.CheckIfExists(q) || !QuestionService.Validate(q))
            {
                return BadRequest();
            }

            QuestionService.Add(q);
            return question;
        }

        // PUT api/quizapp
        [HttpPost("categories")]
        public object AddCategory([FromBody]JObject category)
        {
            Category c = category.ToObject<Category>();
            if(CategoryService.CheckIfExists(c) || !CategoryService.Validate(c))
            {
                return BadRequest();
            }

            CategoryService.Add(c);
            return category;
        }

        // POST api/quizapp/{quiz}
        [HttpPost("sessions")]
        public object CreateSession([FromBody]JObject session)
        {
            Session quiz = session.ToObject<Session>();
            quiz = SessionService.CheckQuizAnswers(quiz);
            SessionService.SaveSession(quiz);
            return quiz;
        }
    }
}
