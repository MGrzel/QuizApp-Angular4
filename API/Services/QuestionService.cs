using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using QuizAppApi.Services;

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
        public Question GetById(Guid? questionId, bool admin = false)
        {
            var question = _context.Questions.Where(q => q.Id == questionId && !q.IsDeleted).Include(q => q.Answers).Include(q => q.CategoryList).ThenInclude(cq => cq.Category).Include(q => q.CategoryList).ThenInclude(cq => cq.Question).FirstOrDefault();

            return question;
        }

        public Question GetByName(string title)
        {
            return _context.Questions.Where(c => c.Title.Trim().ToLower() == title.Trim().ToLower() && !c.IsDeleted).FirstOrDefault();
        }

        ///Returns a list of all questions
        public List<Question> GetList(bool admin = false)
        {
            var questions = _context.Questions.Where(q => !q.IsDeleted).Include(q => q.Answers).Include(q => q.CategoryList).ThenInclude(cq => cq.Category).Include(q => q.CategoryList).ThenInclude(cq => cq.Question).ToList();

            return questions;
        }

        public Answer GetCorrectAnswer(Guid questionId)
        {
            return _answerService.GetCorrectByQuestionId(questionId);
        }

        public List<Question> GetDeletedList()
        {
            return _context.Questions.Where(q => q.IsDeleted).Include(q => q.Answers).ToList();
        }

        ///Returns a list of questions specified by the array of the ids
        public List<Question> GetListById(Guid[] questionIds)
        {
            return _context.Questions.Where(q => questionIds.Contains(q.Id) && !q.IsDeleted).Include(q => q.Answers).ToList();
        }

        ///Returns a list of questions specified by the array of the category ids
        public List<Question> GetListByCategoryId(Guid[] categoryIds)
        {
            var questionIds = _context.CategoryQuestions.Where(cc => categoryIds.Contains(cc.CategoryId) && !cc.IsDeleted).Select(cc => cc.QuestionId).ToList();
            return _context.Questions.Where(q => questionIds.Contains(q.Id) && !q.IsDeleted).Include(q => q.Answers).Distinct().ToList();
        }

        public void Update(Question newQuestion)
        {
            var date = DateTime.Now;
            Question question = GetById(newQuestion.Id);

            question.CreationDate = date;
            question.Title = newQuestion.Title;

            Guid correctAnswerId = newQuestion.Answers.Where(a => a.IsCorrect).Select(a => a.Id).Single();

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
            _context.SaveChanges();

            foreach (CategoryQuestion cat in newCategories)
            {
                _context.CategoryQuestions.Add(new CategoryQuestion
                {
                    CreationDate = date,
                    Category = _categoryService.GetById(cat.Category.Id),
                    Question = question
                });
                _context.SaveChanges();
            }
        }

        public bool CheckIfHasCategory(List<CategoryQuestion> categories, CategoryQuestion category)
        {
            foreach (CategoryQuestion cat in categories)
            {
                if (cat.Category.Id == category.Category.Id)
                    return true;
            }

            return false;
        }

        public void AddToSeed(Question newQuestion)
        {
            _context.Questions.Add(newQuestion);

            _context.SaveChanges();
        }

        public void Add(Question question)
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
                cat.Category = _categoryService.GetById(cat.Category.Id);
            }

            _context.Questions.Add(question);

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var question = GetById(id);
            var date = DateTime.Now;

            question.IsDeleted = true;
            question.DeletionDate = date;

            _context.Questions.Update(question);
            _context.SaveChanges();
        }

        public void Restore(Question question)
        {
            question.IsDeleted = false;
            question.DeletionDate = null;

            _context.Questions.Update(question);
            _context.SaveChanges();
        }

        public bool CheckIfExists(Question question)
        {
            if (GetByName(question.Title) != null)
            {
                return true;
            }

            if (GetById(question.Id) != null)
            {
                return true;
            }

            return false;
        }

        public bool Validate(Question question)
        {
            if (question == null)
            {
                return false;
            }

            if (question.Answers == null)
            {
                return false;
            }

            foreach (var answer in question.Answers)
            {
                if (!_answerService.Validate(answer))
                {
                    return false;
                }
            }

            if (question.Answers.Where(a => a.IsCorrect).FirstOrDefault() == null)
            {
                return false;
            }

            if (question.Title.Trim() == "" || question.Title == null)
            {
                return false;
            }


            return true;
        }
    }
}
