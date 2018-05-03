using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public class ColorService : IColorService
    {
        private readonly QuizAppDb _context;

        public ColorService(QuizAppDb context)
        {
            _context = context;
        }

        ///Returns a color specified by the id
        public Color GetById(int? id)
        {
            return _context.Colors.Where(c => c.Id == id && !c.IsDeleted).FirstOrDefault();
        }

        ///Returns a color specified by the name
        public Color GetByName(string colorName)
        {
            return _context.Colors.Where(c => c.Title == colorName && !c.IsDeleted).FirstOrDefault();
        }

        ///Returns a list of all colors
        public List<Color> GetList()
        {
            return _context.Colors.Where(c => !c.IsDeleted).ToList();
        }

        public void Add(Color color)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Colors.Add(color);

                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Colors ON;");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Colors OFF;");

                transaction.Commit();
            }
        }
    }
}
