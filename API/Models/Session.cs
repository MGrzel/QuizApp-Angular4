using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class Session
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public Challenge Challenge { get; set; }
        public List<ClientQuiz> ClientQuiz { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}