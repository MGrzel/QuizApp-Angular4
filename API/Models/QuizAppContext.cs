using Microsoft.EntityFrameworkCore;
using System.Data;

namespace QuizAppApi.Models
{
    public class QuizAppDb : DbContext
    {
        public QuizAppDb(DbContextOptions<QuizAppDb> options) : base(options)
        { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; } 
        public DbSet<Category> Categories { get; set; }  
        public DbSet<Color> Colors { get; set; } 
        public DbSet<CorrectAnswer> CorrectAnswers { get; set; } 
        public DbSet<Challenge> Challenges { get; set; } 
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ClientQuiz> ClientQuizes { get; set; }
        public DbSet<QuizType> QuizTypes { get; set; }
        public DbSet<ChallengeCategory> ChallengeCategories { get; set; } 
        public DbSet<CategoryQuestion> CategoryQuestions { get; set; } 
    }
}
