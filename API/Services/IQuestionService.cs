using System.Collections.Generic;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IQuestionService
    {
        void Add(Question question);
        void AddToSeed(Question question);
        bool CheckIfExists(Question question);
        void Delete(int id);
        Question GetById(int? questionId, bool admin = false);
        Question GetByName(string title);
        Answer GetCorrectAnswer(int questionId);
        List<Question> GetDeletedList();
        List<Question> GetList(bool admin = false);
        List<Question> GetListByCategoryId(int[] categoryIds);
        List<Question> GetListById(int[] questionIds);
        void Restore(Question question);
        void Update(Question question);
        bool Validate(Question question);
    }
}
