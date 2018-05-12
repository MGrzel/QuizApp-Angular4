using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAppApi.Models;
using QuizAppApi.Services;

namespace QuizAppApi.Controllers
{
    [Route("quizapp")]
    [Authorize("Bearer")]
    public class SessionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ISessionService _sessionService;
        private readonly IChallengeService _challengeService;
        private readonly ICategoryService _categoryService;

        public SessionController(IQuestionService questionService, ISessionService sessionService, IChallengeService challengeService, ICategoryService categoryService)
        {
            _questionService = questionService;
            _sessionService = sessionService;
            _challengeService = challengeService;
            _categoryService = categoryService;
        }

        [HttpGet("get/sessions/{id?}")]
        public async Task<object> GetSessions(Guid? id)
        {
            if (id.HasValue)
            {
                return await _sessionService.GetById(id);
            }
            return await _sessionService.GetList();
        }

        [HttpGet("get/session-active")]
        public async Task<object> GetActiveSession()
        {
            return await _sessionService.GetActive();
        }

        [HttpGet("get/quiz/{id}")]
        public async Task<object> GetQuiz(Guid id)
        {
            Session session = new Session();
            Challenge challenge = await _challengeService.GetById(id);
            var categoryIds = (await _categoryService.GetListByChallengeId(id)).Select(c => c.Id).ToList();
            List<Question> questions = await _questionService.GetListByCategoryId(categoryIds);

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

        [HttpPost("post/sessions")]
        public async Task<object> CreateSession([FromBody]Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _sessionService.SaveSession(session);
            return session;
        }
    }
}
