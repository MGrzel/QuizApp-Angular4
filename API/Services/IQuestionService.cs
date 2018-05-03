using System;
using System.Collections.Generic;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IQuestionService
    {
        void Add(Question question);
        void AddToSeed(Question question);
        bool CheckIfExists(Question question);
        void Delete(Guid id);
        Question GetById(Guid? questionId, bool admin = false);
        Question GetByName(string title);
        Answer GetCorrectAnswer(Guid questionId);
        List<Question> GetDeletedList();
        List<Question> GetList(bool admin = false);
        List<Question> GetListByCategoryId(Guid[] categoryIds);
        List<Question> GetListById(Guid[] questionIds);
        void Restore(Question question);
        void Update(Question question);
        bool Validate(Question question);
    }
}
