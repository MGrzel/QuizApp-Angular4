using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore;
using QuizAppApi.Models;

namespace QuizAppApi.Services
{
    public class AnswerService
    {
        public static List<Answer> GetListByQuestionId(int questionId)
        {
            using (var db = new QuizAppDb())
            {
                return db.Answers.Where(c => c.QuestionId == questionId && !c.IsDeleted).ToList();
            }
        }

        public static Answer GetCorrectByQuestionId(int questionId)
        {
            using (var db = new QuizAppDb())
            {
                var answerId = db.CorrectAnswers.Where(ca => ca.QuestionId == questionId && !ca.IsDeleted).Select(ca => ca.AnswerId).FirstOrDefault();

                return db.Answers.Where(a => a.Id == answerId && !a.IsDeleted).FirstOrDefault();
            }
        }

        public static bool CheckAnswer(Answer answer)
        {
            using (var db = new QuizAppDb())
            {
                if (answer == null)
                {
                    return false;
                }
                List<int> correctAnswers = db.CorrectAnswers.Where(ca => ca.QuestionId == answer.QuestionId).Select(ca => ca.AnswerId).ToList();
                bool isCorrect = correctAnswers.Contains(answer.Id);

                return isCorrect;
            }
        }

        public static bool CheckAnswer(int questionId, int answerId)
        {
            using (var db = new QuizAppDb())
            {
                List<int> correctAnswers = db.CorrectAnswers.Where(ca => ca.QuestionId == questionId).Select(ca => ca.AnswerId).ToList();
                bool isCorrect = correctAnswers.Contains(answerId);

                return isCorrect;
            }
        }

        public static bool Validate(Answer answer)
        {
            if(answer == null)
            {
                return false;
            }

            if (answer.Title.Trim() == "" || answer.Title == null)
            {
                return false;
            }

            return true;
        }
    }
}