using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using System.Threading.Tasks;

namespace QuizAppApi.Services
{
    public class QuizTypeService : IQuizTypeService
    {
        private readonly QuizAppDb _context;

        public QuizTypeService(QuizAppDb context)
        {
            _context = context;
        }
        public async Task<List<QuizType>> GetList()
        {
            return await _context.QuizTypes
                .Where(qt => !qt.IsDeleted)
                .ToListAsync();
        }

        public async Task<QuizType> GetById(Guid id)
        {
            return await _context.QuizTypes
                .Where(qt => qt.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Add(QuizType quizType)
        {
            _context.QuizTypes.Add(quizType);

            await _context.SaveChangesAsync();
        }
    }
}
