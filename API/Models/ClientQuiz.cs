using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuizAppApi.Models
{
    public class ClientQuiz : BaseEntity
    {
        [ForeignKey("SessionId")]
        public Session Session { get; set; }
        public Guid SessionId { get; set; }
        public int Order { get; set; }
        [ForeignKey("SelectedAnswerId")]
        public Answer SelectedAnswer { get; set; }
        public Guid? SelectedAnswerId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
    }
}
