using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public class QuizTypeService
    {
        public static List<QuizType> GetList()
        {
            using (var db = new QuizAppDb())
            {
                return db.QuizTypes.Where(qt => !qt.IsDeleted).ToList();
            }
        }
    }
}