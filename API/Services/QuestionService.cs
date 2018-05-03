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
            var question = _context.Questions.Where(q => q.Id == questionId && !q.IsDeleted).Include(q => q.Answers).FirstOrDefault();

            return question;
        }

        public Question GetByName(string title)
        {
            return _context.Questions.Where(c => c.Title.Trim().ToLower() == title.Trim().ToLower() && !c.IsDeleted).FirstOrDefault();
        }

        ///Returns a list of all questions
        public List<Question> GetList(bool admin = false)
        {
            var questions = _context.Questions.Where(q => !q.IsDeleted).Include(q => q.Answers).ToList();

            return questions;
        }

        public Answer GetCorrectAnswer(Guid questionId)
        {
            var answerId = _context.CorrectAnswers.Where(ca => ca.QuestionId == questionId && !ca.IsDeleted).Select(ca => ca.AnswerId).FirstOrDefault();

            return _context.Answers.Where(a => a.Id == answerId && !a.IsDeleted).FirstOrDefault();
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

        public void Update(Question question)
        {
            var correctAnswer = new CorrectAnswer();
            var newCategories = new List<Category>();
            var oldCategoryQuestion = new List<CategoryQuestion>();
            var newCategoryQuestion = new List<CategoryQuestion>();
            var clientQuizes = new List<ClientQuiz>();
            var date = DateTime.Now;

            //correctAnswer.CreationDate = date;
            //correctAnswer.AnswerId = question.CorrectAnswer.Id;
            //correctAnswer.QuestionId = question.CorrectAnswer.QuestionId;
            //correctAnswer.Id = _context.CorrectAnswers.Where(ca => ca.QuestionId == question.CorrectAnswer.QuestionId).Select(ca => ca.Id).FirstOrDefault();

            //newCategories = question.CategoryList.ToList();
            //oldCategoryQuestion = _context.CategoryQuestions.Where(cq => cq.QuestionId == question.Id && cq.IsDeleted == false).ToList();

            //foreach (var cq in oldCategoryQuestion)
            //{
            //    cq.IsDeleted = true;
            //    cq.DeletionDate = date;
            //}

            //foreach (var cat in newCategories)
            //{
            //    var categoryQuestion = new CategoryQuestion
            //    {
            //        CategoryId = cat.Id,
            //        QuestionId = question.Id,
            //        CreationDate = date
            //    };
            //    newCategoryQuestion.Add(categoryQuestion);
            //}

            //_context.CategoryQuestions.UpdateRange(oldCategoryQuestion);
            //_context.CategoryQuestions.AddRange(newCategoryQuestion);
            //_context.CorrectAnswers.Update(correctAnswer);
            //_context.SaveChanges();
            _context.Questions.Update(question);
            _context.SaveChanges();

            clientQuizes = _context.ClientQuizes.Include(cq => cq.Question).ThenInclude(q => q.Answers).Where(cq => cq.Question.Id == correctAnswer.QuestionId).ToList();
            foreach (var quiz in clientQuizes)
            {
                quiz.Question = _context.Questions.Where(q => q.Id == quiz.Question.Id).Include("Answers").First();
                if (quiz.SelectedAnswerId != null)
                {
                    if (correctAnswer.AnswerId == quiz.SelectedAnswerId)
                    {
                        quiz.IsCorrect = true;
                    }
                    else
                    {
                        quiz.IsCorrect = false;
                    }
                }
            }
            _context.ClientQuizes.UpdateRange(clientQuizes);
            _context.SaveChanges();
        }

        public void AddToSeed(Question question)
        {
            _context.Questions.Add(question);

            _context.SaveChanges();
        }

        public void Add(Question question)
        {
            //var correctAnswer = new CorrectAnswer();
            //var newCategories = new List<Category>();
            //var newCategoryQuestion = new List<CategoryQuestion>();
            //var questionId = _context.Questions.Any() ? _context.Questions.Last().Id + 1 : 1;
            var date = DateTime.Now;

            //newCategories = question.CategoryList.ToList();

            //foreach (var cat in newCategories)
            //{
            //    var categoryQuestion = new CategoryQuestion
            //    {
            //        CategoryId = cat.Id,
            //        QuestionId = questionId,
            //        CreationDate = date
            //    };
            //    newCategoryQuestion.Add(categoryQuestion);
            //}

            //foreach (var answer in question.Answers)
            //{
            //    answer.CreationDate = date;
            //    answer.QuestionId = questionId;
            //    answer.Id = 0;
            //}

            //question.Id = questionId;
            question.CreationDate = date;

            //_context.CategoryQuestions.AddRange(newCategoryQuestion);
            _context.Questions.Add(question);

            _context.SaveChanges();

            //correctAnswer.CreationDate = date;
            //correctAnswer.AnswerId = _context.Answers.Where(a => a.QuestionId == questionId && a.Title == question.CorrectAnswer.Title).Select(a => a.Id).First();
            //correctAnswer.QuestionId = questionId;

            //_context.CorrectAnswers.Add(correctAnswer);
            //_context.SaveChanges();
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

            if (question.CorrectAnswerId == null)
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
