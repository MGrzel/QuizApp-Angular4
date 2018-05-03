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
        void Delete(int id);
        Category GetById(int? categoryId);
        Category GetByName(string title);
        List<Category> GetCategoriesByChallengeId(int challengeId);
        List<Category> GetCategoriesByQuestionId(int questionId);
        List<ChallengeCategory> GetChallengeCategoriesList();
        List<Category> GetDeletedList();
        List<Category> GetList();
        List<Category> GetListByChallengeId(int challengeId);
        List<Category> GetListById(int[] categoryIds);
        List<CategoryQuestion> GetQuestionCategoriesList();
        void Restore(Category category);
        void Update(Category category);
        bool Validate(Category category);
    }
}