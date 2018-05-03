using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class CategoryQuestion
    {
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuestionId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
