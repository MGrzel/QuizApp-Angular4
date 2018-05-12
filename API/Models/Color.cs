using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace QuizAppApi.Models
{
    public class Color : BaseEntity
    {
        public string Value { get; set; }
        public string Title { get; set; }
    }
}
