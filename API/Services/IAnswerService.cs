using System.Collections.Generic;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public interface IAnswerService
    {
        List<Answer> GetList();
        List<CorrectAnswer> GetCorrectAnswersList();
        void AddCorrectAnswer(CorrectAnswer correctAnswer);
        void AddToSeed(Answer answer);
        bool CheckAnswer(Answer answer);
        bool CheckAnswer(int questionId, int answerId);
        Answer GetCorrectByQuestionId(int questionId);
        List<Answer> GetListByQuestionId(int questionId);
        bool Validate(Answer answer);
    }
}
