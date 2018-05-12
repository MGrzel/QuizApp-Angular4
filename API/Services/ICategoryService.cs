using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface ICategoryService
    {
        Task Add(Category category);
        Task AddChallengeCategory(ChallengeCategory category);
        Task AddQuestionCategory(CategoryQuestion category);
        Task<bool> CheckIfExists(Category category);
        Task Delete(Guid id);
        Task<Category> GetById(Guid? categoryId);
        Task<Category> GetByName(string title);
        Task<List<Category>> GetCategoriesByChallengeId(Guid challengeId);
        Task<List<Category>> GetCategoriesByQuestionId(Guid questionId);
        Task<List<ChallengeCategory>> GetChallengeCategoriesList();
        Task<List<Category>> GetDeletedList();
        Task<List<Category>> GetList();
        Task<List<Category>> GetListByChallengeId(Guid challengeId);
        Task<List<Category>> GetListById(List<Guid> categoryIds);
        Task<List<CategoryQuestion>> GetQuestionCategoriesList();
        Task Restore(Category category);
        Task Update(Category newCategory);
        Task<bool> Validate(Category category);
    }
}
