using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using System.Threading.Tasks;

namespace QuizAppApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly QuizAppDb _context;

        public CategoryService(QuizAppDb context)
        {
            _context = context;
        }

        ///Returns a category specified by the id
        public async Task<Category> GetById(Guid? categoryId)
        {
            return await _context.Categories
                .Where(c => categoryId == c.Id && !c.IsDeleted)
                .FirstOrDefaultAsync();
        }

        ///Returns a list of categories specified by the challenge id
        public async Task<List<Category>> GetCategoriesByChallengeId(Guid challengeId)
        {
            var categoryIds = await _context.ChallengeCategories
                .Where(cc => cc.ChallengeId == challengeId && !cc.IsDeleted)
                .Select(cc => cc.CategoryId)
                .ToListAsync();

            return await _context.Categories
                .Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted)
                .ToListAsync();
        }

        ///Returns a list of categories specified by the array of the challenge ids
        public async Task<List<Category>> GetCategoriesByQuestionId(Guid questionId)
        {
            var categoryIds = await _context.CategoryQuestions
                .Where(cc => cc.QuestionId == questionId && !cc.IsDeleted)
                .Select(cc => cc.CategoryId)
                .ToListAsync();

            return await _context.Categories
                .Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Category> GetByName(string title)
        {
            return await _context.Categories
                .Where(c => c.Title.Trim().ToLower() == title.Trim().ToLower() && !c.IsDeleted)
                .FirstOrDefaultAsync();
        }

        ///Returns a list of all categories
        public async Task<List<Category>> GetList()
        {
            return await _context.Categories
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        ///Returns a list of all categories
        public async Task<List<Category>> GetDeletedList()
        {
            return await _context.Categories
                .Where(c => c.IsDeleted)
                .ToListAsync();
        }

        ///Returns a list of categories specified by the array of the ids
        public async Task<List<Category>> GetListById(List<Guid> categoryIds)
        {
            return await _context.Categories
                .Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<CategoryQuestion>> GetQuestionCategoriesList()
        {
            return await _context.CategoryQuestions.ToListAsync();
        }

        public async Task<List<ChallengeCategory>> GetChallengeCategoriesList()
        {
            return await _context.ChallengeCategories.ToListAsync();
        }

        ///Returns a list of categories specified by the array of the challenge ids
        public async Task<List<Category>> GetListByChallengeId(Guid challengeId)
        {
            var categoryIds = await _context.ChallengeCategories
                .Where(cc => cc.ChallengeId == challengeId && !cc.IsDeleted)
                .Select(cc => cc.CategoryId).ToListAsync();

            return await _context.Categories
                .Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted)
                .ToListAsync();
        }


        public async Task Add(Category category)
        {
            var date = DateTime.Now;

            category.CreationDate = date;

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();
        }

        public async Task AddQuestionCategory(CategoryQuestion category)
        {
            var date = DateTime.Now;

            category.CreationDate = date;

            _context.CategoryQuestions.Add(category);

            await _context.SaveChangesAsync();
        }

        public async Task AddChallengeCategory(ChallengeCategory category)
        {
            var date = DateTime.Now;

            category.CreationDate = date;

            _context.ChallengeCategories.Add(category);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Category newCategory)
        {
            var date = DateTime.Now;

            Category category = await GetById(newCategory.Id);
            category.CreationDate = date;
            category.Title = newCategory.Title;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Category category = await GetById(id);
            var date = DateTime.Now;

            category.IsDeleted = true;
            category.DeletionDate = date;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task Restore(Category category)
        {
            category.IsDeleted = false;
            category.DeletionDate = null;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfExists(Category category)
        {
            if (await GetById(category.Id) != null)
            {
                return true;
            }

            return false;
        }

        public Task<bool> Validate(Category category)
        {
            if (category == null)
            {
                return Task.FromResult(false);
            }

            if (category.Title.Trim() == "" || category.Title == null)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
