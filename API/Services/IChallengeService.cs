using System;
using System.Collections.Generic;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IChallengeService
    {
        void Add(Challenge challenge);
        void AddToSeed(Challenge challenge);
        bool CheckIfExists(Challenge challenge);
        void Delete(Guid id);
        Challenge GetById(Guid? challengeId);
        Challenge GetByName(string title);
        List<Challenge> GetDeletedList();
        List<Challenge> GetList();
        void Restore(Challenge challenge);
        void Update(Challenge challenge);
        bool Validate(Challenge challenge);
    }
}