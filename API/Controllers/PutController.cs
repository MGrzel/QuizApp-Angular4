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
        QuizAppDb QuizDb = new QuizAppDb();

        // PUT api/quizapp
        [HttpPut("questions/{id?}")]
        public object UpdateQuestion(int? id, [FromBody]JObject question)
        {
            if (id.HasValue)
            {
                Question q = question.ToObject<Question>();
                if (!QuestionService.CheckIfExists(q) || !QuestionService.Validate(q))
                {
                    return BadRequest();
                }

                QuestionService.Update(q);
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
                if (!ChallengeService.CheckIfExists(c) || !ChallengeService.Validate(c))
                {
                    return BadRequest();
                }

                ChallengeService.Update(c);
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
                if (!CategoryService.CheckIfExists(c) || !CategoryService.Validate(c))
                {
                    return BadRequest();
                }

                CategoryService.Update(c);
                return category;
            }
            return category;
        }
    }
}
