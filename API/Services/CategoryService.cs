using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public class CategoryService
    {
        ///Returns a category specified by the id
        public static Category GetById(int? categoryId)
        {
            using (var db = new QuizAppDb())
            {
                return db.Categories.Where(c => categoryId == c.Id && !c.IsDeleted).FirstOrDefault();
            }
        }

        public static Category GetByName(string title)
        {
            using (var db = new QuizAppDb())
            {
                return db.Categories.Where(c => c.Title.Trim().ToLower() == title.Trim().ToLower() && !c.IsDeleted).FirstOrDefault();
            }
        }

        ///Returns a list of categories specified by the challenge id
        public static List<Category> GetByChallengeId(int challengeId)
        {
            using (var db = new QuizAppDb())
            {
                var categoryIds = db.ChallengeCategories.Where(cc => cc.ChallengeId == challengeId && !cc.IsDeleted).Select(cc => cc.CategoryId).ToList();
                return db.Categories.Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted).ToList();
            }
        }

        ///Returns a list of all categories
        public static List<Category> GetList()
        {
            using (var db = new QuizAppDb())
            {
                return db.Categories.Where(c => !c.IsDeleted).ToList();
            }
        }

        ///Returns a list of all categories
        public static List<Category> GetDeletedList()
        {
            using (var db = new QuizAppDb())
            {
                return db.Categories.Where(c => c.IsDeleted).ToList();
            }
        }

        ///Returns a list of categories specified by the array of the ids
        public static List<Category> GetListById(int[] categoryIds)
        {
            using (var db = new QuizAppDb())
            {
                return db.Categories.Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted).ToList();
            }
        }

        ///Returns a list of categories specified by the array of the challenge ids
        public static List<Category> GetListByChallengeId(int challengeId)
        {
            using (var db = new QuizAppDb())
            {
                var categoryIds = db.ChallengeCategories.Where(cc => cc.ChallengeId == challengeId && !cc.IsDeleted).Select(cc => cc.CategoryId).ToList();
                return db.Categories.Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted).ToList();
            }
        }

        ///Returns a list of categories specified by the array of the challenge ids
        public static List<Category> GetListByQuestionId(int questionId)
        {
            using (var db = new QuizAppDb())
            {
                var categoryIds = db.CategoryQuestions.Where(cc => cc.QuestionId == questionId && !cc.IsDeleted).Select(cc => cc.CategoryId).ToList();
                return db.Categories.Where(c => categoryIds.Contains(c.Id) && !c.IsDeleted).ToList();
            }
        }


        public static void Add(Category category)
        {
            using (var db = new QuizAppDb())
            {
                var categoryId = db.Categories.Any() ? db.Categories.Last().Id + 1 : 1;
                var date = DateTime.Now;

                category.Id = categoryId;
                category.CreationDate = date;

                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        public static void Update(Category category)
        {
            using (var db = new QuizAppDb())
            {
                var date = DateTime.Now;

                category.CreationDate = date;

                db.Categories.Update(category);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (var db = new QuizAppDb())
            {
                var category = GetById(id);
                var date = DateTime.Now;

                category.IsDeleted = true;
                category.DeletionDate = date;

                db.Categories.Update(category);
                db.SaveChanges();
            }
        }

        public static void Restore(Category category)
        {
            using (var db = new QuizAppDb())
            {
                category.IsDeleted = false;
                category.DeletionDate = null;

                db.Categories.Update(category);
                db.SaveChanges();
            }
        }

        public static bool CheckIfExists(Category category)
        {
            using (var db = new QuizAppDb())
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
        }

        public static bool Validate(Category category)
        {
            using (var db = new QuizAppDb())
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
}