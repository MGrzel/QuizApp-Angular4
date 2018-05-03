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

        public SessionService(QuizAppDb context, IAnswerService answerService)
        {
            _context = context;
            _answerService = answerService;
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

            _context.Sessions.Add(quizSession);

            _context.SaveChanges();
        }

        public Session CheckQuizAnswers(Session session)
        {
            foreach (var q in session.ClientQuiz)
            {
                if (_answerService.CheckAnswer(q.QuestionId, q.SelectedAnswerId))
                {
                    q.IsCorrect = true;
                }
                else
                {
                    q.IsCorrect = false;
                }
            }
            return session;
        }
    }
}
