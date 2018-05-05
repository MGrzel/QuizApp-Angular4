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

        public List<Answer> GetCorrectAnswersList()
        {
            return _context.Answers.Where(a => a.IsCorrect).ToList();
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
            Answer isCorrect = _context.Answers.Where(a => a.Id == answer.Id && a.IsCorrect).First();

            if (isCorrect == null)
                return false;
            else
                return true;
        }

        public List<Answer> GetListByQuestionId(Guid questionId)
        {
            return _context.Answers.Where(c => c.QuestionId == questionId && !c.IsDeleted).ToList();
        }

        public Answer GetCorrectByQuestionId(Guid questionId)
        {
            return _context.Answers.Where(a => a.QuestionId == questionId && !a.IsDeleted && a.IsCorrect).FirstOrDefault();
        }

        public bool CheckAnswer(Guid questionId, Guid answerId)
        {
            Answer isCorrect = _context.Answers.Where(ca => ca.QuestionId == questionId && !ca.IsDeleted && ca.IsCorrect && ca.Id == answerId).FirstOrDefault();

            if (isCorrect == null)
                return false;
            else
                return true;
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
