using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAppApi.Models
{
    public class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
            this.QuestionCategories = new HashSet<QuestionCategory>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionCategory> QuestionCategories { get; set; }
        public virtual CorrectAnswer CorrectAnswer { get; set; }
    }
}