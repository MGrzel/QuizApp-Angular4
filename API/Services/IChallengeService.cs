using System.Collections.Generic;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IChallengeService
    {
        void AddToSeed(Challenge challenge);
        void Add(Challenge challenge);
        bool CheckIfExists(Challenge challenge);
        void Delete(int id);
        Challenge GetById(int? challengeId);
        Challenge GetByName(string title);
        List<Challenge> GetDeletedList();
        List<Challenge> GetList();
        void Restore(Challenge challenge);
        void Update(Challenge challenge);
        bool Validate(Challenge challenge);
    }
}
