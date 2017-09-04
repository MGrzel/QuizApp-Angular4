using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAppApi.Models
{
    public class QuestionCategory
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int CategoryId { get; set; }

        public virtual Question Question { get; set; }
        public virtual Category Category { get; set; }
    }
}