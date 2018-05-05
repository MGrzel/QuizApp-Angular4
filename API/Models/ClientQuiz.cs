using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuizAppApi.Models
{
    public class ClientQuiz
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("SessionId")]
        public Session Session { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SessionId { get; set; }
        public int Order { get; set; }
        [ForeignKey("SelectedAnswerId")]
        public Answer SelectedAnswer { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? SelectedAnswerId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuestionId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
