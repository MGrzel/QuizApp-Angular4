using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class Challenge : BaseEntity
    {
        public string Title { get; set; }
        public int QuestionAmount { get; set; }
        [ForeignKey("QuizTypeId")]
        public QuizType QuizType { get; set; }
        public Guid QuizTypeId { get; set; }
        [ForeignKey("ColorId")]
        public Color Color { get; set; }
        public Guid ColorId { get; set; }
        public List<ChallengeCategory> CategoryList { get; set; }
    }
}
