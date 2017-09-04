using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace QuizAppApi.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [ForeignKey("QuestionId")]
        public List<Answer> Answers { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }

        [NotMapped]
        public List<Category> CategoryList { get; set; }

        [NotMapped]
        public Answer CorrectAnswer { get; set; }
    }
}