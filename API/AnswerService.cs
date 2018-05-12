using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using System.Threading.Tasks;

namespace QuizAppApi.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly QuizAppDb _context;

        public AnswerService(QuizAppDb context)
        {
            _context = context;
        }

        public async Task<List<Answer>> GetList()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<List<Answer>> GetCorrectAnswersList()
        {
            return await _context.Answers
                .Where(a => a.IsCorrect)
                .ToListAsync();
        }

        public async Task<bool> CheckAnswer(Answer answer)
        {
            if (answer == null)
            {
                return false;
            }
            Answer isCorrect = await _context.Answers.Where(a => a.Id == answer.Id && a.IsCorrect).FirstOrDefaultAsync();

            if (isCorrect == null)
                return false;
            else
                return true;
        }

        public async Task<List<Answer>> GetListByQuestionId(Guid questionId)
        {
            return await _context.Answers
                .Where(c => c.QuestionId == questionId && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Answer> GetCorrectByQuestionId(Guid questionId)
        {
            return await _context.Answers
                .Where(a => a.QuestionId == questionId && !a.IsDeleted && a.IsCorrect)
                .FirstOrDefaultAsync();
        }

        public async Task<bool>CheckAnswer(Guid answerId)
        {
            Answer isCorrect = await _context.Answers
                .Where(ca => !ca.IsDeleted && ca.IsCorrect && ca.Id == answerId)
                .FirstOrDefaultAsync();

            if (isCorrect == null)
                return false;
            else
                return true;
        }

        public async Task<bool> Validate(Answer answer)
        {
            if(answer == null)
            {
                return await Task.FromResult(false);
            }

            if (answer.Title == null || answer.Title.Trim() == "")
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }
    }
}
