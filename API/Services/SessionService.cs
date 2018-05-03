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
        public Session GetById(int? sessionId)
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
            var sessionId = _context.Sessions.Any() ? _context.Sessions.Last().Id + 1 : 1;
            var quizId = _context.ClientQuizes.Any() ? _context.ClientQuizes.Last().Id + 1 : 1;
            var creationDate = DateTime.Now;

            quizSession.Challenge = _context.Challenges.Where(c => c.Id == quizSession.Challenge.Id).Include("Color").Include("QuizType").FirstOrDefault();
            quizSession.Id = sessionId;
            quizSession.CreationDate = creationDate;

            foreach (var quiz in quizSession.ClientQuiz)
            {
                quiz.Id = quizId;
                quiz.SessionId = quizSession.Id;
                quiz.CreationDate = creationDate;
                quiz.Question = _context.Questions.Where(c => c.Id == quiz.Question.Id).Include("Answers").FirstOrDefault();
                quizId++;
            }

            quizSession.ClientQuiz = quizSession.ClientQuiz.ToList();
            _context.Sessions.Add(quizSession);
            _context.SaveChanges();
        }

        public Session CheckQuizAnswers(Session session)
        {
            foreach (var q in session.ClientQuiz)
            {
                if (_answerService.CheckAnswer(q.SelectedAnswer))
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
