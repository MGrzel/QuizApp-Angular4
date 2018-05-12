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
        public async Task<Challenge> GetById(Guid? challengeId)
        {
            return await _context.Challenges
                .Where(c => c.Id == challengeId && !c.IsDeleted)
                .Include("Color").Include("QuizType")
                .Include(c => c.CategoryList)
                    .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync();
        }

        public async Task<Challenge> GetByName(string title)
        {
            return await _context.Challenges
                .Where(c => c.Title.Trim().ToLower() == title.Trim().ToLower() && !c.IsDeleted)
                .FirstOrDefaultAsync();
        }

        ///Returns a list of all challenges
        public async Task<List<Challenge>> GetList()
        {
            return await _context.Challenges
                .Where(c => !c.IsDeleted).Include("Color").Include("QuizType")
                .Include(c => c.CategoryList)
                    .ThenInclude(c => c.Category)
                .ToListAsync();
        }

        ///Returns a list of all challenges
        public async Task<List<Challenge>> GetDeletedList()
        {
            return await _context.Challenges
                .Where(c => c.IsDeleted).Include("Color")
                .Include("QuizType")
                .ToListAsync();
        }

        public async Task Add(Challenge challenge)
        {
            var date = DateTime.Now;
            challenge.QuizType = await _context.QuizTypes
                .Where(qt => qt.Id == challenge.QuizType.Id)
                .FirstOrDefaultAsync();

            challenge.Color = await _context.Colors
                .Where(c => c.Id == challenge.Color.Id)
                .FirstOrDefaultAsync();

            challenge.CreationDate = date;

            foreach (ChallengeCategory cat in challenge.CategoryList)
            {
                cat.CategoryId = Guid.Empty;
                cat.ChallengeId = Guid.Empty;
                cat.Category = await _categoryService.GetById(cat.Category.Id);
            }

            _context.Challenges.Add(challenge);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Challenge newChallenge)
        {
            var date = DateTime.Now;

            Challenge challenge = await GetById(newChallenge.Id);

            challenge.QuizType = await _context.QuizTypes
                .Where(qt => qt.Id == newChallenge.QuizType.Id)
                .FirstOrDefaultAsync();

            challenge.Color = await _context.Colors
                .Where(c => c.Id == newChallenge.Color.Id)
                .FirstOrDefaultAsync();

            challenge.CreationDate = date;
            challenge.QuestionAmount = newChallenge.QuestionAmount;
            challenge.Title = newChallenge.Title;

            List<ChallengeCategory> newCategories = newChallenge.CategoryList;
            challenge.CategoryList = null;

            _context.Challenges.Update(challenge);
            await _context.SaveChangesAsync();

            foreach (ChallengeCategory category in newCategories)
            {
                _context.ChallengeCategories.Add(new ChallengeCategory
                {
                    CreationDate = date,
                    Category = await _categoryService.GetById(category.Category.Id),
                    Challenge = challenge
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid id)
        {
            Challenge challenge = await GetById(id);
            var date = DateTime.Now;

            challenge.IsDeleted = true;
            challenge.DeletionDate = date;

            _context.Challenges.Update(challenge);
            await _context.SaveChangesAsync();
        }

        public async Task Restore(Challenge challenge)
        {
            challenge.IsDeleted = false;
            challenge.DeletionDate = null;

            _context.Challenges.Update(challenge);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfExists(Challenge challenge)
        {
            if (await GetByName(challenge.Title) != null)
            {
                return true;
            }

            if (await GetById(challenge.Id) != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Validate(Challenge challenge)
        {
            if (challenge == null)
            {
                return await Task.FromResult(false);
            }

            if (challenge.Title.Trim() == "" || challenge.Title == null)
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }
    }
}
