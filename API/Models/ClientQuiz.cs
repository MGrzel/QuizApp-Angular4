using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class ClientQuiz
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public bool IsCorrect { get; set; }
        public int Order { get; set; }
        public Answer SelectedAnswer { get; set; } 

        public Question Question { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}