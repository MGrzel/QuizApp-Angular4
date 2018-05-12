using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IColorService
    {
        Task Add(Color color);
        Task<Color> GetById(Guid? id);
        Task<Color> GetByName(string colorName);
        Task<List<Color>> GetList();
    }
}