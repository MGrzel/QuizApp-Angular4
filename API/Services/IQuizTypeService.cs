using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IQuizTypeService
    {
        Task Add(QuizType quizType);
        Task<QuizType> GetById(Guid id);
        Task<List<QuizType>> GetList();
    }
}