using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class Session : BaseEntity
    {
        public bool IsActive { get; set; }
        [ForeignKey("ChallengeId")]
        public Challenge Challenge { get; set; }
        public Guid ChallengeId { get; set; }
        public List<ClientQuiz> ClientQuiz { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
