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
        QuizAppDb QuizDb = new QuizAppDb();

        // DELETE api/quizapp/5
        [HttpPatch("questions/{id}")]
        public void RestoreQuestion(int id, [FromBody]JObject question)
        {
            Question q = question.ToObject<Question>();
            QuestionService.Restore(q);
        }

        // DELETE api/quizapp/5
        [HttpPatch("categories/{id}")]
        public void RestoreCategory(int id, [FromBody]JObject category)
        {
            Category c = category.ToObject<Category>();
            CategoryService.Restore(c);
        }

        // DELETE api/quizapp/5
        [HttpPatch("challenges/{id}")]
        public void RestoreChallenge(int id, [FromBody]JObject challenge)
        {
            Challenge c = challenge.ToObject<Challenge>();
            ChallengeService.Restore(c);
        }
    }
}
