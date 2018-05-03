using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class Challenge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int QuestionAmount { get; set; }
        [ForeignKey("QuizTypeId")]
        public QuizType QuizType { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuizTypeId { get; set; }
        [ForeignKey("ColorId")]
        public Color Color { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ColorId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
        public List<ChallengeCategory> CategoryList { get; set; }
    }
}
