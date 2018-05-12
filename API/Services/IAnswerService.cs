using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IAnswerService
    {
        Task<bool> CheckAnswer(Answer answer);
        Task<bool> CheckAnswer(Guid answerId);
        Task<List<Answer>> GetCorrectAnswersList();
        Task<Answer> GetCorrectByQuestionId(Guid questionId);
        Task<List<Answer>> GetList();
        Task<List<Answer>> GetListByQuestionId(Guid questionId);
        Task<bool> Validate(Answer answer);
    }
}
