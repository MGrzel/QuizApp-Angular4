using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace QuizAppApi.Models
{
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Answer> Answers { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
        public List<CategoryQuestion> CategoryList { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CorrectAnswerId { get; set; }
    }
}
