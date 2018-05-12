using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using QuizAppApi.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace QuizAppApi.Services
{
    public class SessionService : ISessionService
    {
        private readonly QuizAppDb _context;
        private readonly IAnswerService _answerService;
        private readonly IChallengeService _challengeService;
        private readonly IQuestionService _questionService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAccountService _accountService;

        public SessionService(QuizAppDb context, IAnswerService answerService, IChallengeService challengeService, IQuestionService questionService, IHttpContextAccessor httpContext, IAccountService accountService)
        {
            _context = context;
            _answerService = answerService;
            _challengeService = challengeService;
            _questionService = questionService;
            _httpContext = httpContext;
            _accountService = accountService;
        }
        ///Returns a session specified by the id
        public async Task<Session> GetById(Guid? sessionId)
        {
            Session session = await _context.Sessions
                    .Where(s => s.Id == sessionId && !s.IsDeleted)
                    .Include(s => s.Challenge.Color)
                    .Include(s => s.Challenge.QuizType)
                    .Include(s => s.ClientQuiz)
                        .ThenInclude(c => c.Question)
                            .ThenInclude(q => q.Answers)
                    .FirstOrDefaultAsync();

            session.ClientQuiz = await _context.ClientQuizes
                .Where(s => s.SessionId == sessionId && !s.IsDeleted)
                .OrderBy(cq => cq.Order)
                .Include(c => c.Question)
                    .ThenInclude(q => q.Answers).ToListAsync();

            return session;
        }

        ///Returns an active session specified by the id
        public async Task<Session> GetActive()
        {
            return await _context.Sessions
                .Where(s => s.IsActive == true && !s.IsDeleted)
                .FirstOrDefaultAsync();
        }

        ///Returns a list of all session
        public async Task<List<Session>> GetList()
        {
            string codedToken = _httpContext.HttpContext
                .Request
                .Headers["Authorization"];

            codedToken = codedToken.Replace("Bearer ", "");
            User user = await _accountService.GetUserFromJwtToken(codedToken);

            return await _context.Sessions
                    .Where(s => !s.IsDeleted && s.User == user)
                    .Include(s => s.Challenge.Color)
                    .Include(s => s.Challenge.QuizType)
                    .Include(s => s.ClientQuiz)
                    .OrderByDescending(s => s.CreationDate)
                    .ToListAsync();
        }

        public async Task SaveSession(Session quizSession)
        {
            var creationDate = DateTime.Now;

            string codedToken = _httpContext.HttpContext
                .Request
                .Headers["Authorization"];

            codedToken = codedToken.Replace("Bearer ", "");

            quizSession.User = await _accountService.GetUserFromJwtToken(codedToken);

            quizSession.CreationDate = creationDate;

            quizSession.ChallengeId = Guid.Empty;
            quizSession.Challenge = await _challengeService.GetById(quizSession.Challenge.Id);
            foreach(ClientQuiz quiz in quizSession.ClientQuiz)
            {
                quiz.Id = Guid.Empty;
                quiz.CreationDate = creationDate;
                quiz.QuestionId = Guid.Empty;
                quiz.Question = await _questionService.GetById(quiz.Question.Id);
                quiz.SelectedAnswerId = null;
                if(quiz.SelectedAnswer != null)
                {
                    quiz.SelectedAnswer = (await _answerService.GetList()).Where(a => a.Id == quiz.SelectedAnswer.Id).FirstOrDefault();
                }
            }

            _context.Sessions.Add(quizSession);

            await _context.SaveChangesAsync();
        }
    }
}
