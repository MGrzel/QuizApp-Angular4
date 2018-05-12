using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class ChallengeCategory : BaseEntity
    {
        [ForeignKey("ChallengeId")]
        public Challenge Challenge { get; set; }
        public Guid ChallengeId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
