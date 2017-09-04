using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAppApi.Models
{
    public class CorrectAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        
        public virtual Question Question { get; set; }
        public virtual Answer Answer { get; set; }
    }
}