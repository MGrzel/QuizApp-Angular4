using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface ISessionService
    {
        Task<Session> GetActive();
        Task<Session> GetById(Guid? sessionId);
        Task<List<Session>> GetList();
        Task SaveSession(Session quizSession);
    }
}