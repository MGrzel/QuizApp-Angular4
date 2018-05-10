using System;
using System.Collections.Generic;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface ICategoryService
    {
        void Add(Category category);
        void AddChallengeCategory(ChallengeCategory category);
        void AddQuestionCategory(CategoryQuestion category);
        bool CheckIfExists(Category category);
        void Delete(Guid id);
        Category GetById(Guid categoryId);
        Category GetByName(string title);
        List<Category> GetCategoriesByChallengeId(Guid challengeId);
        List<Category> GetCategoriesByQuestionId(Guid questionId);
        List<ChallengeCategory> GetChallengeCategoriesList();
        List<Category> GetDeletedList();
        List<Category> GetList();
        List<Category> GetListByChallengeId(Guid challengeId);
        List<Category> GetListById(Guid[] categoryIds);
        List<CategoryQuestion> GetQuestionCategoriesList();
        void Restore(Category category);
        void Update(Category category);
        bool Validate(Category category);
    }
}
