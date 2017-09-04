using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAppApi.Models
{
    public class ChallengeCategory
    {
        public int Id { get; set; }
        public int ChallengeId { get; set; }
        public int CategoryId { get; set; }

        public virtual Challenge Challenge { get; set; }
        public virtual Category Category { get; set; }
    }
}