﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using QuizAppApi.Models;
using System;

namespace QuizAppApi.Migrations
{
    [DbContext(typeof(QuizAppDb))]
    partial class QuizAppDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuizAppApi.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("QuizAppApi.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("QuizAppApi.Models.CategoryQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("QuestionId");

                    b.HasKey("Id");

                    b.ToTable("CategoryQuestions");
                });

            modelBuilder.Entity("QuizAppApi.Models.Challenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ColorId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("QuestionAmount");

                    b.Property<int?>("QuizTypeId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("QuizTypeId");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("QuizAppApi.Models.ChallengeCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("ChallengeId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("ChallengeCategories");
                });

            modelBuilder.Entity("QuizAppApi.Models.ClientQuiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<bool>("IsCorrect");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Order");

                    b.Property<int?>("QuestionId");

                    b.Property<int?>("SelectedAnswerId");

                    b.Property<int>("SessionId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SelectedAnswerId");

                    b.HasIndex("SessionId");

                    b.ToTable("ClientQuizes");
                });

            modelBuilder.Entity("QuizAppApi.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("QuizAppApi.Models.CorrectAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("QuestionId");

                    b.HasKey("Id");

                    b.ToTable("CorrectAnswers");
                });

            modelBuilder.Entity("QuizAppApi.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuizAppApi.Models.QuizType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("QuizTypes");
                });

            modelBuilder.Entity("QuizAppApi.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ChallengeId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeletionDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("QuizAppApi.Models.Answer", b =>
                {
                    b.HasOne("QuizAppApi.Models.Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizAppApi.Models.Challenge", b =>
                {
                    b.HasOne("QuizAppApi.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId");

                    b.HasOne("QuizAppApi.Models.QuizType", "QuizType")
                        .WithMany()
                        .HasForeignKey("QuizTypeId");
                });

            modelBuilder.Entity("QuizAppApi.Models.ClientQuiz", b =>
                {
                    b.HasOne("QuizAppApi.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.HasOne("QuizAppApi.Models.Answer", "SelectedAnswer")
                        .WithMany()
                        .HasForeignKey("SelectedAnswerId");

                    b.HasOne("QuizAppApi.Models.Session")
                        .WithMany("ClientQuiz")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QuizAppApi.Models.Session", b =>
                {
                    b.HasOne("QuizAppApi.Models.Challenge", "Challenge")
                        .WithMany()
                        .HasForeignKey("ChallengeId");
                });
#pragma warning restore 612, 618
        }
    }
}
