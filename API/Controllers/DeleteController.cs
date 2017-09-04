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
        QuizAppDb QuizDb = new QuizAppDb();

        // DELETE api/quizapp/5
        [HttpDelete("questions/{id}")]
        public void DeleteQuestion(int id)
        {
            QuestionService.Delete(id);
        }

        // DELETE api/quizapp/5
        [HttpDelete("categories/{id}")]
        public void DeleteCategory(int id)
        {
            CategoryService.Delete(id);
        }

        // DELETE api/quizapp/5
        [HttpDelete("challenges/{id}")]
        public void DeleteChallenge(int id)
        {
            ChallengeService.Delete(id);
        }
    }
}
