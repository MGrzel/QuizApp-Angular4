using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using QuizAppApi.Services;

namespace QuizAppApi.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly QuizAppDb _context;
        private readonly ICategoryService _categoryService;

        public ChallengeService(QuizAppDb context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        ///Returns a challenge specified by the id
        public Challenge GetById(Guid? challengeId)
        {
            var challenge = _context.Challenges.Where(c => c.Id == challengeId && !c.IsDeleted).Include("Color").Include("QuizType").FirstOrDefault();

            return challenge;
        }

        public Challenge GetByName(string title)
        {
            return _context.Challenges.Where(c => c.Title.Trim().ToLower() == title.Trim().ToLower() && !c.IsDeleted).FirstOrDefault();
        }

        ///Returns a list of all challenges
        public List<Challenge> GetList()
        {
            var challenges = _context.Challenges.Where(c => !c.IsDeleted).Include("Color").Include("QuizType").ToList();

            return challenges;
        }

        ///Returns a list of all challenges
        public List<Challenge> GetDeletedList()
        {
            return _context.Challenges.Where(c => c.IsDeleted).Include("Color").Include("QuizType").ToList();
        }

        public void AddToSeed(Challenge challenge)
        {
            _context.Challenges.Add(challenge);

            _context.SaveChanges();
        }

        public void Add(Challenge challenge)
        {

            var challengeCategories = new List<ChallengeCategory>();
            var date = DateTime.Now;
            //challenge.QuizType = _context.QuizTypes.Where(qt => qt.Id == challenge.QuizType.Id).First();
            //challenge.Color = _context.Colors.Where(c => c.Id == challenge.Color.Id).First();
            challenge.CreationDate = date;

            //foreach (var cat in challenge.CategoryList)
            //{
            //    var category = new ChallengeCategory
            //    {
            //        CategoryId = cat.Id,
            //        ChallengeId = challengeId,
            //        CreationDate = date
            //    };
            //    challengeCategories.Add(category);
            //}

            _context.Challenges.Add(challenge);
            //_context.ChallengeCategories.AddRange(challengeCategories);
            _context.SaveChanges();
        }

        public void Update(Challenge challenge)
        {
            var newChallengeCategories = new List<ChallengeCategory>();
            var oldChallengeCategories = new List<ChallengeCategory>();
            var date = DateTime.Now;

            //challenge.QuizType = _context.QuizTypes.Where(qt => qt.Id == challenge.QuizType.Id).First();
            //challenge.Color = _context.Colors.Where(c => c.Id == challenge.Color.Id).First();
            challenge.CreationDate = date;

            //oldChallengeCategories = _context.ChallengeCategories.Where(cq => cq.ChallengeId == challenge.Id && cq.IsDeleted == false).ToList();

            //foreach (var cq in oldChallengeCategories)
            //{
            //    cq.IsDeleted = true;
            //    cq.DeletionDate = date;
            //}

            //foreach (var cat in challenge.CategoryList)
            //{
            //    var category = new ChallengeCategory
            //    {
            //        CategoryId = cat.Id,
            //        ChallengeId = challenge.Id,
            //        CreationDate = date
            //    };
            //    newChallengeCategories.Add(category);
            //}

            _context.Challenges.Update(challenge);
            //_context.ChallengeCategories.UpdateRange(oldChallengeCategories);
            //_context.ChallengeCategories.AddRange(newChallengeCategories);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var challenge = GetById(id);
            var date = DateTime.Now;

            challenge.IsDeleted = true;
            challenge.DeletionDate = date;

            _context.Challenges.Update(challenge);
            _context.SaveChanges();
        }

        public void Restore(Challenge challenge)
        {
            challenge.IsDeleted = false;
            challenge.DeletionDate = null;

            _context.Challenges.Update(challenge);
            _context.SaveChanges();
        }

        public bool CheckIfExists(Challenge challenge)
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

        public bool Validate(Challenge challenge)
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
