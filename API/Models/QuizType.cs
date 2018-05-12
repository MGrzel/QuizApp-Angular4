using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class QuizType : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
