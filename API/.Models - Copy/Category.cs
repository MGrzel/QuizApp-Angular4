using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAppApi.Models
{
    public class Category
    {
        public Category()
        {
            this.ChallengeCategories = new HashSet<ChallengeCategory>();
            this.QuestionCategories = new HashSet<QuestionCategory>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ChallengeCategory> ChallengeCategories { get; set; }
        public virtual ICollection<QuestionCategory> QuestionCategories { get; set; }
    }
}