using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using QuizAppApi.Services;
using System.Threading.Tasks;

namespace QuizAppApi.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly QuizAppDb _context;
        private readonly IAnswerService _answerService;
        private readonly ICategoryService _categoryService;

        public QuestionService(QuizAppDb context, IAnswerService answerService, ICategoryService categoryService)
        {
            _context = context;
            _answerService = answerService;
            _categoryService = categoryService;
        }

        ///Returns a question specified by the id
        public async Task<Question> GetById(Guid? questionId, bool admin = false)
        {
            return await _context.Questions
                .Where(q => q.Id == questionId && !q.IsDeleted)
                .Include(q => q.Answers)
                .Include(q => q.CategoryList)
                    .ThenInclude(cq => cq.Category)
                .Include(q => q.CategoryList)
                    .ThenInclude(cq => cq.Question)
                .FirstOrDefaultAsync();
        }

        public async Task<Question> GetByName(string title)
        {
            return await _context.Questions
                .Where(c => c.Title.Trim().ToLower() == title.Trim().ToLower() && !c.IsDeleted)
                .FirstOrDefaultAsync();
        }

        ///Returns a list of all questions
        public async Task<List<Question>> GetList(bool admin = false)
        {
            return await _context.Questions
                .Where(q => !q.IsDeleted)
                .Include(q => q.Answers)
                .Include(q => q.CategoryList)
                    .ThenInclude(cq => cq.Category)
                .Include(q => q.CategoryList)
                    .ThenInclude(cq => cq.Question)
                .ToListAsync();
        }

        public async Task<Answer> GetCorrectAnswer(Guid questionId)
        {
            return await _answerService.GetCorrectByQuestionId(questionId);
        }

        public async Task<List<Question>> GetDeletedList()
        {
            return await _context.Questions
                .Where(q => q.IsDeleted)
                .Include(q => q.Answers)
                .ToListAsync();
        }

        ///Returns a list of questions specified by the array of the ids
        public async Task<List<Question>> GetListById(List<Guid> questionIds)
        {
            return await _context.Questions
                .Where(q => questionIds.Contains(q.Id) && !q.IsDeleted)
                .Include(q => q.Answers)
                .ToListAsync();
        }

        ///Returns a list of questions specified by the array of the category ids
        public async Task<List<Question>> GetListByCategoryId(List<Guid> categoryIds)
        {
            var questionIds = await _context.CategoryQuestions
                .Where(cc => categoryIds.Contains(cc.CategoryId) && !cc.IsDeleted)
                .Select(cc => cc.QuestionId)
                .ToListAsync();

            return await _context.Questions
                .Where(q => questionIds.Contains(q.Id) && !q.IsDeleted)
                .Include(q => q.Answers).Distinct()
                .ToListAsync();
        }

        public async Task Update(Question newQuestion)
        {
            var date = DateTime.Now;
            Question question = await GetById(newQuestion.Id);

            question.CreationDate = date;
            question.Title = newQuestion.Title;

            Guid correctAnswerId = newQuestion.Answers
                .Where(a => a.IsCorrect)
                .Select(a => a.Id)
                .SingleOrDefault();

            foreach (Answer answer in question.Answers)
            {
                if (answer.Id == correctAnswerId)
                {
                    answer.IsCorrect = true;
                }
                else
                {
                    answer.IsCorrect = false;
                }
            }

            List<CategoryQuestion> newCategories = newQuestion.CategoryList;
            List<CategoryQuestion> oldCategories = question.CategoryList;
            question.CategoryList = null;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();

            foreach (CategoryQuestion cat in newCategories)
            {
                _context.CategoryQuestions.Add(new CategoryQuestion
                {
                    CreationDate = date,
                    Category = await _categoryService.GetById(cat.Category.Id),
                    Question = question
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CheckIfHasCategory(List<CategoryQuestion> categories, CategoryQuestion category)
        {
            foreach (CategoryQuestion cat in categories)
            {
                if (cat.Category.Id == category.Category.Id)
                    return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task Add(Question question)
        {
            var date = DateTime.Now;

            question.CreationDate = date;
            question.Id = Guid.Empty;
            foreach (Answer answer in question.Answers)
            {
                answer.Id = Guid.Empty;
            }

            foreach (CategoryQuestion cat in question.CategoryList)
            {
                cat.CategoryId = Guid.Empty;
                cat.QuestionId = Guid.Empty;
                cat.Category = await _categoryService.GetById(cat.Category.Id);
            }

            _context.Questions.Add(question);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Question question = await GetById(id);
            var date = DateTime.Now;

            question.IsDeleted = true;
            question.DeletionDate = date;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task Restore(Question question)
        {
            question.IsDeleted = false;
            question.DeletionDate = null;

            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfExists(Question question)
        {
            if (await GetByName(question.Title) != null)
            {
                return true;
            }

            if (await GetById(question.Id) != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Validate(Question question)
        {
            if (question == null)
            {
                return await Task.FromResult(false);
            }

            if (question.Answers == null)
            {
                return await Task.FromResult(false);
            }

            foreach (var answer in question.Answers)
            {
                if (! await _answerService.Validate(answer))
                {
                    return await Task.FromResult(false);
                }
            }

            if (!question.Answers.Where(a => a.IsCorrect).Any())
            {
                return await Task.FromResult(false);
            }

            if (question.Title.Trim() == "" || question.Title == null)
            {
                return await Task.FromResult(false);
            }


            return await Task.FromResult(true);
        }
    }
}
