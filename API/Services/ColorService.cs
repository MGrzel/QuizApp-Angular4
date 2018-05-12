using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;
using System.Threading.Tasks;

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
        public async Task<Color> GetById(Guid? id)
        {
            return await _context.Colors
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();
        }

        ///Returns a color specified by the name
        public async Task<Color> GetByName(string colorName)
        {
            return await _context.Colors
                .Where(c => c.Title == colorName && !c.IsDeleted)
                .FirstOrDefaultAsync();
        }

        ///Returns a list of all colors
        public async Task<List<Color>> GetList()
        {
            return await _context.Colors
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task Add(Color color)
        {
            _context.Colors.Add(color);

            await _context.SaveChangesAsync();
        }
    }
}
