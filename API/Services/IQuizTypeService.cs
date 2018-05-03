using System.Collections.Generic;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IQuizTypeService
    {
        void Add(QuizType quizType);
        List<QuizType> GetList();
        QuizType GetById(int id);
    }
}
