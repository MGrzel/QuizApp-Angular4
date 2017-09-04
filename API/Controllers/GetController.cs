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

    public class GetController : Controller
    {
        QuizAppDb QuizDb = new QuizAppDb();

        // GET api/quizapp/get/categories/id?
        [HttpGet("categories/{id?}")]
        public object GetCategories(int? id)
        {
            if (id.HasValue)
            {
                return CategoryService.GetById(id);
            }
            return CategoryService.GetList();
        }

        // GET api/quizapp/get/categories/id?
        [HttpGet("deleted/categories")]
        public object GetDeletedCategories()
        {
            return CategoryService.GetDeletedList();
        }

        // GET api/quizapp/get/categories/id?
        [HttpGet("correctanswer/{questionId}/{answerId}")]
        public object CheckSingleAnswer(int questionId, int answerId)
        {
            return AnswerService.CheckAnswer(questionId, answerId);
        }

        // GET api/quizapp/get/challenges/id?
        [HttpGet("challenges/{id?}")]
        public object GetChallenges(int? id)
        {
            if (id.HasValue)
            {
                return ChallengeService.GetById(id);
            }
            return ChallengeService.GetList();
        }

        // GET api/quizapp/get/challenges/id?
        [HttpGet("deleted/challenges/")]
        public object GetDeletedChallenges()
        {
                return ChallengeService.GetDeletedList();
        }

        // GET api/quizapp/get/colors/name
        [HttpGet("colors/{name}")]
        public object GetColorsByName(string name)
        {
            return ColorService.GetByName(name);
        }

        // GET api/quizapp/get/colors/id?
        [HttpGet("colors/{id?}")]
        public object GetColors(int? id)
        {
            if (id.HasValue)
            {
                return ColorService.GetById(id);
            }
            return ColorService.GetList();
        }

        // GET api/quizapp/get/questions/id?
        [HttpGet("questions/{id?}")]
        public object GetQuestions(int? id)
        {
            if (id.HasValue)
            {
                return QuestionService.GetById(id);
            }
            return QuestionService.GetList();
        }

        // GET api/quizapp/get/questions/id?
        [HttpGet("deleted/questions/")]
        public object GetDeletedQuestions()
        {
                return QuestionService.GetDeletedList();
        }

        // GET api/quizapp/get/questions/id?
        [HttpGet("admin/questions/{id?}")]
        public object GetQuestionsAsAdmin(int? id)
        {
            if (id.HasValue)
            {
                return QuestionService.GetById(id, true);
            }
            return QuestionService.GetList(true);
        }

        // GET api/quizapp/get/questions/id?
        [HttpGet("quiztypes")]
        public object GetQuizTypeList()
        {
            return QuizTypeService.GetList();
        }

        // GET api/quizapp/get/correctanswer/id?
        [HttpGet("correctanswer/{id}")]
        public object GetCorrectAnswer(int id)
        {
            return AnswerService.GetCorrectByQuestionId(id);
        }


        // GET api/quizapp/get/sessions/id?
        [HttpGet("sessions/{id?}")]
        public object GetSessions(int? id)
        {
            if (id.HasValue)
            {
                return SessionService.GetById(id);
            }
            return SessionService.GetList();
        }

        // GET api/quizapp/get/session/id?
        [HttpGet("session-active")]
        public object GetActiveSession()
        {
            return SessionService.GetActive();
        }

        // GET api/quizapp/get/quiz/id
        [HttpGet("quiz/{id}")]
        public object GetQuiz(int id)
        {
            Session session = new Session();
            var challenge = ChallengeService.GetById(id);
            var categoryIds = CategoryService.GetListByChallengeId(id).Select(c => c.Id).ToArray();
            var questions = QuestionService.GetListByCategoryId(categoryIds).ToList();

            Random rnd = new Random();
            if (questions.Count() > challenge.QuestionAmount)
            {
                questions = questions.OrderBy(q => Guid.NewGuid()).Take(challenge.QuestionAmount).ToList();
            }
            else
            {
                questions = questions.OrderBy(q => Guid.NewGuid()).ToList();
            }

            List<ClientQuiz> clientQuizes = new List<ClientQuiz>();

            int count = 1;
            foreach (Question question in questions)
            {
                ClientQuiz clientQuiz = new ClientQuiz();
                question.Answers = question.Answers.OrderBy(q => Guid.NewGuid()).ToList();
                clientQuiz.Question = question;
                clientQuiz.Order = count;
                clientQuiz.IsCorrect = false;
                clientQuizes.Add(clientQuiz);

                count++;
            }

            session.Challenge = challenge;
            session.ClientQuiz = clientQuizes;
            session.CreationDate = DateTime.Now;

            return session;
        }
    }
}
