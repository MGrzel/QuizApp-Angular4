using System;
using System.Collections.Generic;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IColorService
    {
        Color GetById(Guid? id);
        Color GetByName(string colorName);
        List<Color> GetList();
        void Add(Color color);
    }
}
