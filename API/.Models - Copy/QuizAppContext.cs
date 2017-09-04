using Microsoft.EntityFrameworkCore;
using System.Data;

namespace QuizAppApi.Models
{
    public class QuizAppDb : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; } 
        public DbSet<Category> Categories { get; set; }  
        public DbSet<CorrectAnswer> CorrectAnswers { get; set; } 
        public DbSet<Challenge> Challenges { get; set; } 
        public DbSet<ChallengeCategory> ChallengeCategories { get; set; } 
        public DbSet<QuestionCategory> QuestionCategories { get; set; } 
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./QuizApp.db");
        }
    }
}