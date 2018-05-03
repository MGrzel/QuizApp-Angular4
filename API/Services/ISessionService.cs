using System;
using System.Collections.Generic;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface ISessionService
    {
        Session CheckQuizAnswers(Session session);
        Session GetActive();
        Session GetById(Guid? sessionId);
        List<Session> GetList();
        void SaveSession(Session quizSession);
    }
}