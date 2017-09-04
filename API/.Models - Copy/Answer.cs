using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAppApi.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }

        public virtual Question Question { get; set; }
        public virtual CorrectAnswer CorrectAnswer { get; set; }
    }
}