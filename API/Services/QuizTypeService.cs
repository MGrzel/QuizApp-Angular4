using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public class QuizTypeService : IQuizTypeService
    {
        private readonly QuizAppDb _context;

        public QuizTypeService(QuizAppDb context)
        {
            _context = context;
        }
        public List<QuizType> GetList()
        {
            return _context.QuizTypes.Where(qt => !qt.IsDeleted).ToList();
        }

        public QuizType GetById(Guid id)
        {
            return _context.QuizTypes.Where(qt => qt.Id == id).FirstOrDefault();
        }

        public void Add(QuizType quizType)
        {
            _context.QuizTypes.Add(quizType);

            _context.SaveChanges();
        }
    }
}
