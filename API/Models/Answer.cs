using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace QuizAppApi.Models
{
    public class Answer : BaseEntity
    {
        public Guid QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
    }
}
