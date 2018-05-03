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

        public QuizType GetById(int id)
        {
            return _context.QuizTypes.Where(qt => qt.Id == id).FirstOrDefault();
        }

        public void Add(QuizType quizType)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.QuizTypes.Add(quizType);
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.QuizTypes ON;");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.QuizTypes OFF;");

                transaction.Commit();
            }
        }
    }
}
