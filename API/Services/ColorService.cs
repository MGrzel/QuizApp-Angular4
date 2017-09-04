using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public class ColorService
    {
        ///Returns a color specified by the id
        public static Color GetById(int? id)
        {
            using (var db = new QuizAppDb())
            {
                return db.Colors.Where(c => c.Id == id && !c.IsDeleted).FirstOrDefault();
            }
        }

        ///Returns a color specified by the name
        public static Color GetByName(string colorName)
        {
            using (var db = new QuizAppDb())
            {
                return db.Colors.Where(c => c.Title == colorName && !c.IsDeleted).FirstOrDefault();
            }
        }

        ///Returns a list of all colors
        public static List<Color> GetList()
        {
            using (var db = new QuizAppDb())
            {
                return db.Colors.Where(c => !c.IsDeleted).ToList();
            }
        }
    }
}