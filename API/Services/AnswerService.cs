using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly QuizAppDb _context;

        public AnswerService(QuizAppDb context)
        {
            _context = context;
        }

        public List<Answer> GetList()
        {
            return _context.Answers.ToList();
        }

        public List<CorrectAnswer> GetCorrectAnswersList()
        {
            return _context.CorrectAnswers.ToList();
        }

        public void AddCorrectAnswer(CorrectAnswer correctAnswer)
        {
            _context.CorrectAnswers.Add(correctAnswer);
            _context.SaveChanges();
        }

        public void AddToSeed(Answer answer)
        {
            _context.Answers.Add(answer);
            _context.SaveChanges();
        }

        public bool CheckAnswer(Answer answer)
        {
            if (answer == null)
            {
                return false;
            }
            List<Guid> correctAnswers = _context.CorrectAnswers.Where(ca => ca.QuestionId == answer.QuestionId).Select(ca => ca.AnswerId).ToList();
            bool isCorrect = correctAnswers.Contains(answer.Id);

            return isCorrect;
        }

        public List<Answer> GetListByQuestionId(Guid questionId)
        {
            return _context.Answers.Where(c => c.QuestionId == questionId && !c.IsDeleted).ToList();
        }

        public Answer GetCorrectByQuestionId(Guid questionId)
        {
            var answerId = _context.CorrectAnswers.Where(ca => ca.QuestionId == questionId && !ca.IsDeleted).Select(ca => ca.AnswerId).FirstOrDefault();

            return _context.Answers.Where(a => a.Id == answerId && !a.IsDeleted).FirstOrDefault();
        }

        public bool CheckAnswer(Guid questionId, Guid answerId)
        {
            List<Guid> correctAnswers = _context.CorrectAnswers.Where(ca => ca.QuestionId == questionId).Select(ca => ca.AnswerId).ToList();
            bool isCorrect = correctAnswers.Contains(answerId);

            return isCorrect;
        }

        public bool Validate(Answer answer)
        {
            if(answer == null)
            {
                return false;
            }

            if (answer.Title == null || answer.Title.Trim() == "")
            {
                return false;
            }

            return true;
        }
    }
}
