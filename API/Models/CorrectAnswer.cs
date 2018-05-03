using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class CorrectAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuestionId { get; set; }
        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AnswerId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
