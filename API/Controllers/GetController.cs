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
using Microsoft.AspNetCore.Authorization;

namespace QuizAppApi.Controllers
{
    [Route("quizapp/[controller]")]
    [Authorize("Bearer")]
    public class GetController : Controller
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;
        private readonly ISessionService _sessionService;
        private readonly IChallengeService _challengeService;
        private readonly ICategoryService _categoryService;
        private readonly IColorService _colorService;
        private readonly IQuizTypeService _quizTypeService;

        public GetController(IAnswerService answerService, IQuestionService questionService, ISessionService sessionService, IChallengeService challengeService, ICategoryService categoryService, IColorService colorService, IQuizTypeService quizTypeService)
        {
            _answerService = answerService;
            _questionService = questionService;
            _sessionService = sessionService;
            _challengeService = challengeService;
            _categoryService = categoryService;
            _colorService = colorService;
            _quizTypeService = quizTypeService;
        }

        // GET api/quizapp/get/categories/id?
        [HttpGet("categories/{id?}")]
        public object GetCategories(Guid? id)
        {
            if (id.HasValue)
            {
                return _categoryService.GetById(id);
            }
            return _categoryService.GetList();
        }

        // GET api/quizapp/get/categories/id?
        [HttpGet("deleted/categories")]
        public object GetDeletedCategories()
        {
            return _categoryService.GetDeletedList();
        }

        // GET api/quizapp/get/categories/id?
        [HttpGet("correctanswer/{questionId}/{answerId}")]
        public object CheckSingleAnswer(Guid questionId, Guid answerId)
        {
            return _answerService.CheckAnswer(questionId, answerId);
        }

        // GET api/quizapp/get/challenges/id?
        [HttpGet("challenges/{id?}")]
        [AllowAnonymous]
        public object GetChallenges(Guid? id)
        {
            if (id.HasValue)
            {
                return _challengeService.GetById(id);
            }
            return _challengeService.GetList();
        }

        // GET api/quizapp/get/challenges/id?
        [HttpGet("deleted/challenges/")]
        public object GetDeletedChallenges()
        {
                return _challengeService.GetDeletedList();
        }

        // GET api/quizapp/get/colors/name
        [HttpGet("colors/{name}")]
        public object GetColorsByName(string name)
        {
            return _colorService.GetByName(name);
        }

        // GET api/quizapp/get/colors/id?
        [HttpGet("colors/{id?}")]
        public object GetColors(Guid? id)
        {
            if (id.HasValue)
            {
                return _colorService.GetById(id);
            }
            return _colorService.GetList();
        }

        // GET api/quizapp/get/questions/id?
        [HttpGet("questions/{id?}")]
        public object GetQuestions(Guid? id)
        {
            if (id.HasValue)
            {
                return _questionService.GetById(id);
            }
            return _questionService.GetList();
        }

        // GET api/quizapp/get/questions/id?
        [HttpGet("deleted/questions/")]
        public object GetDeletedQuestions()
        {
                return _questionService.GetDeletedList();
        }

        // GET api/quizapp/get/questions/id?
        [HttpGet("admin/questions/{id?}")]
        public object GetQuestionsAsAdmin(Guid? id)
        {
            if (id.HasValue)
            {
                return _questionService.GetById(id, true);
            }
            return _questionService.GetList(true);
        }

        // GET api/quizapp/get/questions/id?
        [HttpGet("quiztypes")]
        public object GetQuizTypeList()
        {
            return _quizTypeService.GetList();
        }

        // GET api/quizapp/get/correctanswer/id?
        [HttpGet("correctanswer/{id}")]
        public object GetCorrectAnswer(Guid id)
        {
            return _answerService.GetCorrectByQuestionId(id);
        }


        // GET api/quizapp/get/sessions/id?
        [HttpGet("sessions/{id?}")]
        public object GetSessions(Guid? id)
        {
            if (id.HasValue)
            {
                return _sessionService.GetById(id);
            }
            return _sessionService.GetList();
        }

        // GET api/quizapp/get/session/id?
        [HttpGet("session-active")]
        public object GetActiveSession()
        {
            return _sessionService.GetActive();
        }

        // GET api/quizapp/get/quiz/id
        [HttpGet("quiz/{id}")]
        public object GetQuiz(Guid id)
        {
            Session session = new Session();
            var challenge = _challengeService.GetById(id);
            var categoryIds = _categoryService.GetListByChallengeId(id).Select(c => c.Id).ToArray();
            var questions = _questionService.GetListByCategoryId(categoryIds).ToList();

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
