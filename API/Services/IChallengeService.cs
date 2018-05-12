using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IChallengeService
    {
        Task Add(Challenge challenge);
        Task<bool> CheckIfExists(Challenge challenge);
        Task Delete(Guid id);
        Task<Challenge> GetById(Guid? challengeId);
        Task<Challenge> GetByName(string title);
        Task<List<Challenge>> GetDeletedList();
        Task<List<Challenge>> GetList();
        Task Restore(Challenge challenge);
        Task Update(Challenge newChallenge);
        Task<bool> Validate(Challenge challenge);
    }
}