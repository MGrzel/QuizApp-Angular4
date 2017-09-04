using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using QuizAppApi.Services;

namespace QuizAppApi.Services
{
    public class ChallengeService
    {
        ///Returns a challenge specified by the id
        public static Challenge GetById(int? challengeId)
        {
            using (var db = new QuizAppDb())
            {
                var challenge = db.Challenges.Where(c => c.Id == challengeId && !c.IsDeleted).Include("Color").Include("QuizType").FirstOrDefault();
                if(challenge != null)
                {
                    challenge.CategoryList = CategoryService.GetByChallengeId(challenge.Id);
                }
                return challenge;
            }
        }

        public static Challenge GetByName(string title)
        {
            using (var db = new QuizAppDb())
            {
                return db.Challenges.Where(c => c.Title.Trim().ToLower() == title.Trim().ToLower() && !c.IsDeleted).FirstOrDefault();
            }
        }

        ///Returns a list of all challenges
        public static List<Challenge> GetList()
        {
            using (var db = new QuizAppDb())
            {
                var challenges = db.Challenges.Where(c => !c.IsDeleted).Include("Color").Include("QuizType").ToList();
                foreach (var challenge in challenges)
                {
                    challenge.CategoryList = CategoryService.GetByChallengeId(challenge.Id);
                }

                return challenges;
            }
        }

        ///Returns a list of all challenges
        public static List<Challenge> GetDeletedList()
        {
            using (var db = new QuizAppDb())
            {
                return db.Challenges.Where(c => c.IsDeleted).Include("Color").Include("QuizType").ToList();
            }
        }

        public static void Add(Challenge challenge)
        {

            using (var db = new QuizAppDb())
            {
                var challengeCategories = new List<ChallengeCategory>();
                var challengeId = db.Challenges.Any() ? db.Challenges.Last().Id + 1 : 1;
                var date = DateTime.Now;
                challenge.QuizType = db.QuizTypes.Where(qt => qt.Id == challenge.QuizType.Id).First();
                challenge.Color = db.Colors.Where(c => c.Id == challenge.Color.Id).First();
                challenge.CreationDate = date;
                challenge.Id = challengeId;

                foreach (var cat in challenge.CategoryList)
                {
                    var category = new ChallengeCategory();
                    category.CategoryId = cat.Id;
                    category.ChallengeId = challengeId;
                    category.CreationDate = date;
                    challengeCategories.Add(category);
                }

                db.Challenges.Add(challenge);
                db.ChallengeCategories.AddRange(challengeCategories);
                db.SaveChanges();
            }
        }

        public static void Update(Challenge challenge)
        {
            using (var db = new QuizAppDb())
            {
                var newChallengeCategories = new List<ChallengeCategory>();
                var oldChallengeCategories = new List<ChallengeCategory>();
                var date = DateTime.Now;

                challenge.QuizType = db.QuizTypes.Where(qt => qt.Id == challenge.QuizType.Id).First();
                challenge.Color = db.Colors.Where(c => c.Id == challenge.Color.Id).First();
                challenge.CreationDate = date;

                oldChallengeCategories = db.ChallengeCategories.Where(cq => cq.ChallengeId == challenge.Id && cq.IsDeleted == false).ToList();

                foreach (var cq in oldChallengeCategories)
                {
                    cq.IsDeleted = true;
                    cq.DeletionDate = date;
                }

                foreach (var cat in challenge.CategoryList)
                {
                    var category = new ChallengeCategory();
                    category.CategoryId = cat.Id;
                    category.ChallengeId = challenge.Id;
                    category.CreationDate = date;
                    newChallengeCategories.Add(category);
                }

                db.Challenges.Update(challenge);
                db.ChallengeCategories.UpdateRange(oldChallengeCategories);
                db.ChallengeCategories.AddRange(newChallengeCategories);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (var db = new QuizAppDb())
            {
                var challenge = GetById(id);
                var date = DateTime.Now;

                challenge.IsDeleted = true;
                challenge.DeletionDate = date;

                db.Challenges.Update(challenge);
                db.SaveChanges();
            }
        }

        public static void Restore(Challenge challenge)
        {
            using (var db = new QuizAppDb())
            {
                challenge.IsDeleted = false;
                challenge.DeletionDate = null;

                db.Challenges.Update(challenge);
                db.SaveChanges();
            }
        }

        public static bool CheckIfExists(Challenge challenge)
        {
            using (var db = new QuizAppDb())
            {
                if (GetByName(challenge.Title) != null)
                {
                    return true;
                }

                if (GetById(challenge.Id) != null)
                {
                    return true;
                }

                return false;
            }
        }

        public static bool Validate(Challenge challenge)
        {
            using (var db = new QuizAppDb())
            {
                if (challenge == null)
                {
                    return false;
                }

                if (challenge.Title.Trim() == "" || challenge.Title == null)
                {
                    return false;
                }

                return true;
            }
        }
    }
}