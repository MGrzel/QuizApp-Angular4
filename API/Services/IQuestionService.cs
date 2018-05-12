using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IQuestionService
    {
        Task Add(Question question);
        Task<bool> CheckIfExists(Question question);
        Task<bool> CheckIfHasCategory(List<CategoryQuestion> categories, CategoryQuestion category);
        Task Delete(Guid id);
        Task<Question> GetById(Guid? questionId, bool admin = false);
        Task<Question> GetByName(string title);
        Task<Answer> GetCorrectAnswer(Guid questionId);
        Task<List<Question>> GetDeletedList();
        Task<List<Question>> GetList(bool admin = false);
        Task<List<Question>> GetListByCategoryId(List<Guid> categoryIds);
        Task<List<Question>> GetListById(List<Guid> questionIds);
        Task Restore(Question question);
        Task Update(Question newQuestion);
        Task<bool> Validate(Question question);
    }
}
