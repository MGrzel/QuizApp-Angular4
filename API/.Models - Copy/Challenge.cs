using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAppApi.Models
{
    public class Challenge
    {
        public Challenge()
        {
            this.ChallengeCategories = new HashSet<ChallengeCategory>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ChallengeCategory> ChallengeCategories { get; set; }
    }
}