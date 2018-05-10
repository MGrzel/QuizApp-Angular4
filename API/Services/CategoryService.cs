using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;

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
        public Category GetById(Guid categoryId)
        {
            return _context.Categories.Where(c => categoryId == c.Id && !c.IsDeleted).FirstOrDefault();
        }

        ///Returns a list of categories specified by the challenge id
        public List<Category> GetCategoriesByChallengeId(Guid challengeId)
        {
            var categoryIds = _context.ChallengeCategories.Where(cc => cc.ChallengeId == challengeId && !cc.IsDeleted).Select(cc => cc.CategoryId).ToList();
            return _context.Categories.Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted).ToList();
        }

        ///Returns a list of categories specified by the array of the challenge ids
        public List<Category> GetCategoriesByQuestionId(Guid questionId)
        {
            var categoryIds = _context.CategoryQuestions.Where(cc => cc.QuestionId == questionId && !cc.IsDeleted).Select(cc => cc.CategoryId).ToList();
            return _context.Categories.Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted).ToList();
        }

        public Category GetByName(string title)
        {
            return _context.Categories.Where(c => c.Title.Trim().ToLower() == title.Trim().ToLower() && !c.IsDeleted).FirstOrDefault();
        }

        ///Returns a list of all categories
        public List<Category> GetList()
        {
            return _context.Categories.Where(c => !c.IsDeleted).ToList();
        }

        ///Returns a list of all categories
        public List<Category> GetDeletedList()
        {
            return _context.Categories.Where(c => c.IsDeleted).ToList();
        }

        ///Returns a list of categories specified by the array of the ids
        public List<Category> GetListById(Guid[] categoryIds)
        {
            return _context.Categories.Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted).ToList();
        }

        public List<CategoryQuestion> GetQuestionCategoriesList()
        {
            return _context.CategoryQuestions.ToList();
        }

        public List<ChallengeCategory> GetChallengeCategoriesList()
        {
            return _context.ChallengeCategories.ToList();
        }

        ///Returns a list of categories specified by the array of the challenge ids
        public List<Category> GetListByChallengeId(Guid challengeId)
        {
            var categoryIds = _context.ChallengeCategories.Where(cc => cc.ChallengeId == challengeId && !cc.IsDeleted).Select(cc => cc.CategoryId).ToList();
            return _context.Categories.Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted).ToList();
        }


        public void Add(Category category)
        {
            var date = DateTime.Now;

            category.CreationDate = date;

            _context.Categories.Add(category);

            _context.SaveChanges();
        }

        public void AddQuestionCategory(CategoryQuestion category)
        {
            var date = DateTime.Now;

            category.CreationDate = date;

            _context.CategoryQuestions.Add(category);

            _context.SaveChanges();
        }

        public void AddChallengeCategory(ChallengeCategory category)
        {
            var date = DateTime.Now;

            category.CreationDate = date;

            _context.ChallengeCategories.Add(category);

            _context.SaveChanges();
        }

        public void Update(Category newCategory)
        {
            var date = DateTime.Now;

            Category category = GetById(newCategory.Id);
            category.CreationDate = date;
            category.Title = newCategory.Title;

            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var category = GetById(id);
            var date = DateTime.Now;

            category.IsDeleted = true;
            category.DeletionDate = date;

            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Restore(Category category)
        {
            category.IsDeleted = false;
            category.DeletionDate = null;

            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public bool CheckIfExists(Category category)
        {
            if (GetByName(category.Title) != null)
            {
                return true;
            }

            if (GetById(category.Id) != null)
            {
                return true;
            }

            return false;
        }

        public bool Validate(Category category)
        {
            if (category == null)
            {
                return false;
            }

            if (category.Title.Trim() == "" || category.Title == null)
            {
                return false;
            }

            return true;
        }
    }
}
