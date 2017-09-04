using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using QuizAppApi.Services;

namespace QuizAppApi.Services
{
    public class SessionService
    {
        ///Returns a session specified by the id
        public static Session GetById(int? sessionId)
        {
            using (var db = new QuizAppDb())
            {
                var session = db.Sessions
                    .Where(s => s.Id == sessionId && !s.IsDeleted)
                    .Include(s => s.Challenge.Color)
                    .Include(s => s.Challenge.QuizType)
                    .Include(s => s.ClientQuiz)
                        .ThenInclude(c => c.Question)
                            .ThenInclude(q => q.Answers)
                    .FirstOrDefault();

                session.ClientQuiz = db.ClientQuizes
                    .Where(s => s.SessionId == sessionId && !s.IsDeleted)
                    .OrderBy(cq => cq.Order)
                    .Include(c => c.Question)
                        .ThenInclude(q => q.Answers).ToList();

                return session;
            }
        }

        ///Returns an active session specified by the id
        public static Session GetActive()
        {
            using (var db = new QuizAppDb())
            {
                return db.Sessions.Where(s => s.IsActive == true && !s.IsDeleted).FirstOrDefault();
            }
        }

        ///Returns a list of all session
        public static List<Session> GetList()
        {
            using (var db = new QuizAppDb())
            {
                return db.Sessions
                    .Where(s => !s.IsDeleted)
                    .Include(s => s.Challenge.Color)
                    .Include(s => s.Challenge.QuizType)
                    .Include(s => s.ClientQuiz)
                    .OrderByDescending(s => s.CreationDate)
                    .ToList();
            }
        }

        public static void SaveSession(Session quizSession)
        {
            using (var db = new QuizAppDb())
            {
                var sessionId = db.Sessions.Any() ? db.Sessions.Last().Id + 1 : 1;
                var quizId = db.ClientQuizes.Any() ? db.ClientQuizes.Last().Id + 1 : 1;
                var creationDate = DateTime.Now;

                quizSession.Challenge = db.Challenges.Where(c => c.Id == quizSession.Challenge.Id).Include("Color").Include("QuizType").FirstOrDefault();
                quizSession.Id = sessionId;
                quizSession.CreationDate = creationDate;

                foreach (var quiz in quizSession.ClientQuiz)
                {
                    quiz.Id = quizId;
                    quiz.SessionId = quizSession.Id;
                    quiz.CreationDate = creationDate;
                    quiz.Question = db.Questions.Where(c => c.Id == quiz.Question.Id).Include("Answers").FirstOrDefault();
                    quizId++;
                }

                quizSession.ClientQuiz = quizSession.ClientQuiz.ToList();
                db.Sessions.Add(quizSession);
                db.SaveChanges();
            }
        }

        public static Session CheckQuizAnswers(Session session)
        {
            foreach (var q in session.ClientQuiz)
            {
                if (AnswerService.CheckAnswer(q.SelectedAnswer))
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