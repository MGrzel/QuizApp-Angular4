using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using QuizAppApi.Models;

namespace QuizAppApi.Models
{
    public class QuizAppDb : IdentityDbContext<User>
    {
        public QuizAppDb(DbContextOptions<QuizAppDb> options) : base(options)
        { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; } 
        public DbSet<Category> Categories { get; set; }  
        public DbSet<Color> Colors { get; set; } 
        public DbSet<Challenge> Challenges { get; set; } 
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ClientQuiz> ClientQuizes { get; set; }
        public DbSet<QuizType> QuizTypes { get; set; }
        public DbSet<ChallengeCategory> ChallengeCategories { get; set; } 
        public DbSet<CategoryQuestion> CategoryQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryQuestion>()
                .HasKey(c => new { c.CategoryId, c.QuestionId });

            modelBuilder.Entity<CategoryQuestion>()
                .HasOne(c => c.Question)
                .WithMany(c => c.CategoryList)
                .HasForeignKey(c => c.QuestionId);

            modelBuilder.Entity<CategoryQuestion>()
                .HasOne(c => c.Category)
                .WithMany(c => c.CategoryQuestions)
                .HasForeignKey(c => c.CategoryId);


            modelBuilder.Entity<ChallengeCategory>()
                .HasKey(c => new { c.CategoryId, c.ChallengeId });

            modelBuilder.Entity<ChallengeCategory>()
                .HasOne(c => c.Challenge)
                .WithMany(c => c.CategoryList)
                .HasForeignKey(c => c.ChallengeId);

            modelBuilder.Entity<ChallengeCategory>()
                .HasOne(c => c.Category)
                .WithMany(c => c.ChallengeCategories)
                .HasForeignKey(c => c.CategoryId);

            //modelBuilder.Entity<ClientQuiz>()
            //    .HasOne(ca => ca.SelectedAnswer)
            //    .WithOne()
            //    .HasForeignKey<ClientQuiz>(ca => ca.SelectedAnswerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question);
        }

        public DbSet<QuizAppApi.Models.User> User { get; set; }
    }
}
