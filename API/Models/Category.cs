using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        public List<ChallengeCategory> ChallengeCategories { get; set; }
        public List<CategoryQuestion> CategoryQuestions { get; set; }
    }
}
