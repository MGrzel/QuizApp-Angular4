using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using QuizAppApi.Services;

namespace QuizAppApi.Services
{
    public class SessionService : ISessionService
    {
        private readonly QuizAppDb _context;
        private readonly IAnswerService _answerService;
        private readonly IChallengeService _challengeService;
        private readonly IQuestionService _questionService;

        public SessionService(QuizAppDb context, IAnswerService answerService, IChallengeService challengeService, IQuestionService questionService)
        {
            _context = context;
            _answerService = answerService;
            _challengeService = challengeService;
            _questionService = questionService;
        }
        ///Returns a session specified by the id
        public Session GetById(Guid? sessionId)
        {
            var session = _context.Sessions
                    .Where(s => s.Id == sessionId && !s.IsDeleted)
                    .Include(s => s.Challenge.Color)
                    .Include(s => s.Challenge.QuizType)
                    .Include(s => s.ClientQuiz)
                        .ThenInclude(c => c.Question)
                            .ThenInclude(q => q.Answers)
                    .FirstOrDefault();

            session.ClientQuiz = _context.ClientQuizes
                .Where(s => s.SessionId == sessionId && !s.IsDeleted)
                .OrderBy(cq => cq.Order)
                .Include(c => c.Question)
                    .ThenInclude(q => q.Answers).ToList();

            return session;
        }

        ///Returns an active session specified by the id
        public Session GetActive()
        {
            return _context.Sessions.Where(s => s.IsActive == true && !s.IsDeleted).FirstOrDefault();
        }

        ///Returns a list of all session
        public List<Session> GetList()
        {
            return _context.Sessions
                    .Where(s => !s.IsDeleted)
                    .Include(s => s.Challenge.Color)
                    .Include(s => s.Challenge.QuizType)
                    .Include(s => s.ClientQuiz)
                    .OrderByDescending(s => s.CreationDate)
                    .ToList();
        }

        public void SaveSession(Session quizSession)
        {
            var creationDate = DateTime.Now;

            quizSession.CreationDate = creationDate;

            quizSession.ChallengeId = Guid.Empty;
            quizSession.Challenge = _challengeService.GetById(quizSession.Challenge.Id);
            foreach(ClientQuiz quiz in quizSession.ClientQuiz)
            {
                quiz.Id = Guid.Empty;
                quiz.CreationDate = creationDate;
                quiz.QuestionId = Guid.Empty;
                quiz.Question = _questionService.GetById(quiz.Question.Id);
                quiz.SelectedAnswerId = null;
                if(quiz.SelectedAnswer != null)
                {
                    quiz.SelectedAnswer = _answerService.GetList().Where(a => a.Id == quiz.SelectedAnswer.Id).FirstOrDefault();
                }
            }

            _context.Sessions.Add(quizSession);

            _context.SaveChanges();
        }
    }
}
