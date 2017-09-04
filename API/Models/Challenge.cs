using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int QuestionAmount { get; set; }
        public QuizType QuizType { get; set; }
        public Color Color { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
        
        [NotMapped]
        public List<Category> CategoryList { get; set; }
    }
}