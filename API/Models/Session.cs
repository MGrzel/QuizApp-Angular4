using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class Session
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("ChallengeId")]
        public Challenge Challenge { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ChallengeId { get; set; }
        public List<ClientQuiz> ClientQuiz { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
