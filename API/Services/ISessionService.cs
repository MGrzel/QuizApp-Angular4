using System;
using System.Collections.Generic;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface ISessionService
    {
        Session GetActive();
        Session GetById(Guid? sessionId);
        List<Session> GetList();
        void SaveSession(Session quizSession);
    }
}
