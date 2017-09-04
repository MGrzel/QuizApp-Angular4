using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using QuizAppApi.Services;

namespace QuizAppApi.Services
{
    public class QuestionService
    {
        ///Returns a question specified by the id
        public static Question GetById(int? questionId, bool admin = false)
        {
            using (var db = new QuizAppDb())
            {
                var question = db.Questions.Where(q => q.Id == questionId && !q.IsDeleted).Include(q => q.Answers).FirstOrDefault();
                if (admin)
                {
                    question.CategoryList = CategoryService.GetListByQuestionId(question.Id);
                }

                return question;
            }
        }

        public static Question GetByName(string title)
        {
            using (var db = new QuizAppDb())
            {
                return db.Questions.Where(c => c.Title.Trim().ToLower() == title.Trim().ToLower() && !c.IsDeleted).FirstOrDefault();
            }
        }

        ///Returns a list of all questions
        public static List<Question> GetList(bool admin = false)
        {
            using (var db = new QuizAppDb())
            {
                var questions = db.Questions.Where(q => !q.IsDeleted).Include(q => q.Answers).ToList();

                if (admin)
                {
                    foreach (var question in questions)
                    {
                        question.CategoryList = CategoryService.GetListByQuestionId(question.Id);
                        question.CorrectAnswer = AnswerService.GetCorrectByQuestionId(question.Id);
                    }
                }

                return questions;
            }
        }

        public static List<Question> GetDeletedList()
        {
            using (var db = new QuizAppDb())
            {
                return db.Questions.Where(q => q.IsDeleted).Include(q => q.Answers).ToList();
            }
        }

        ///Returns a list of questions specified by the array of the ids
        public static List<Question> GetListById(int[] questionIds)
        {
            using (var db = new QuizAppDb())
            {
                return db.Questions.Where(q => questionIds.Contains(q.Id) && !q.IsDeleted).Include(q => q.Answers).ToList();
            }
        }

        ///Returns a list of questions specified by the array of the category ids
        public static List<Question> GetListByCategoryId(int[] categoryIds)
        {
            using (var db = new QuizAppDb())
            {
                var questionIds = db.CategoryQuestions.Where(cc => categoryIds.Contains(cc.CategoryId) && !cc.IsDeleted).Select(cc => cc.QuestionId).ToList();
                return db.Questions.Where(q => questionIds.Contains(q.Id) && !q.IsDeleted).Include(q => q.Answers).Distinct().ToList();
            }
        }

        public static void Update(Question question)
        {
            var correctAnswer = new CorrectAnswer();
            var newCategories = new List<Category>();
            var oldCategoryQuestion = new List<CategoryQuestion>();
            var newCategoryQuestion = new List<CategoryQuestion>();
            var clientQuizes = new List<ClientQuiz>();
            var date = DateTime.Now;

            using (var db = new QuizAppDb())
            {
                correctAnswer.CreationDate = date;
                correctAnswer.AnswerId = question.CorrectAnswer.Id;
                correctAnswer.QuestionId = question.CorrectAnswer.QuestionId;
                correctAnswer.Id = db.CorrectAnswers.Where(ca => ca.QuestionId == question.CorrectAnswer.QuestionId).Select(ca => ca.Id).FirstOrDefault();

                newCategories = question.CategoryList.ToList();
                oldCategoryQuestion = db.CategoryQuestions.Where(cq => cq.QuestionId == question.Id && cq.IsDeleted == false).ToList();

                foreach (var cq in oldCategoryQuestion)
                {
                    cq.IsDeleted = true;
                    cq.DeletionDate = date;
                }

                foreach (var cat in newCategories)
                {
                    var categoryQuestion = new CategoryQuestion();
                    categoryQuestion.CategoryId = cat.Id;
                    categoryQuestion.QuestionId = question.Id;
                    categoryQuestion.CreationDate = date;
                    newCategoryQuestion.Add(categoryQuestion);
                }

                db.CategoryQuestions.UpdateRange(oldCategoryQuestion);
                db.CategoryQuestions.AddRange(newCategoryQuestion);
                db.CorrectAnswers.Update(correctAnswer);
                db.Questions.Update(question);
                db.SaveChanges();
            }

            using (var db = new QuizAppDb())
            {
                clientQuizes = db.ClientQuizes.Include(cq => cq.Question).ThenInclude(q => q.Answers).Where(cq => cq.Question.Id == correctAnswer.QuestionId).ToList();
                foreach (var quiz in clientQuizes)
                {
                    quiz.Question = db.Questions.Where(q => q.Id == quiz.Question.Id).Include("Answers").First();
                    if (quiz.SelectedAnswer != null)
                    {
                        if (correctAnswer.AnswerId == quiz.SelectedAnswer.Id)
                        {
                            quiz.IsCorrect = true;
                        }
                        else
                        {
                            quiz.IsCorrect = false;
                        }
                    }
                }
                db.ClientQuizes.UpdateRange(clientQuizes);
                db.SaveChanges();
            }
        }

        public static void Add(Question question)
        {
            using (var db = new QuizAppDb())
            {
                var correctAnswer = new CorrectAnswer();
                var newCategories = new List<Category>();
                var newCategoryQuestion = new List<CategoryQuestion>();
                var questionId = db.Questions.Any() ? db.Questions.Last().Id + 1 : 1;
                var date = DateTime.Now;

                newCategories = question.CategoryList.ToList();

                foreach (var cat in newCategories)
                {
                    var categoryQuestion = new CategoryQuestion();
                    categoryQuestion.CategoryId = cat.Id;
                    categoryQuestion.QuestionId = questionId;
                    categoryQuestion.CreationDate = date;
                    newCategoryQuestion.Add(categoryQuestion);
                }

                foreach (var answer in question.Answers)
                {
                    answer.CreationDate = date;
                    answer.QuestionId = questionId;
                    answer.Id = 0;
                }

                question.Id = questionId;
                question.CreationDate = date;

                db.CategoryQuestions.AddRange(newCategoryQuestion);
                db.Questions.Add(question);

                db.SaveChanges();

                correctAnswer.CreationDate = date;
                correctAnswer.AnswerId = db.Answers.Where(a => a.QuestionId == questionId && a.Title == question.CorrectAnswer.Title).Select(a => a.Id).First();
                correctAnswer.QuestionId = questionId;

                db.CorrectAnswers.Add(correctAnswer);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (var db = new QuizAppDb())
            {
                var question = GetById(id);
                var date = DateTime.Now;

                question.IsDeleted = true;
                question.DeletionDate = date;

                db.Questions.Update(question);
                db.SaveChanges();
            }
        }

        public static void Restore(Question question)
        {
            using (var db = new QuizAppDb())
            {
                question.IsDeleted = false;
                question.DeletionDate = null;

                db.Questions.Update(question);
                db.SaveChanges();
            }
        }

        public static bool CheckIfExists(Question question)
        {
            using (var db = new QuizAppDb())
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
        }

        public static bool Validate(Question question)
        {
            using (var db = new QuizAppDb())
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
                    if (!AnswerService.Validate(answer))
                    {
                        return false;
                    }
                }

                if (question.CorrectAnswer == null)
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
}