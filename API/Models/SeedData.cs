using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using QuizAppApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuizAppApi.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            IQuestionService questionService;
            IChallengeService challengeService;
            ICategoryService categoryService;
            IColorService colorService;
            IQuizTypeService quizTypeService;
            IAnswerService answerService;

            questionService = serviceProvider.GetRequiredService<IQuestionService>();
            challengeService = serviceProvider.GetRequiredService<IChallengeService>();
            colorService = serviceProvider.GetRequiredService<IColorService>();
            categoryService = serviceProvider.GetRequiredService<ICategoryService>();
            quizTypeService = serviceProvider.GetRequiredService<IQuizTypeService>();
            answerService = serviceProvider.GetRequiredService<IAnswerService>();

            DateTime creationDate = DateTime.Now;

            if (quizTypeService.GetList().Count() == 0)
            {
                QuizType standard = new QuizType
                {
                    CreationDate = creationDate,
                    Description = "In standard quiz you have access to all of the questions immediately. Submiting the quiz means checking all the answers and ending the session.",
                    Title = "standard",
                    IsDeleted = false
                };

                QuizType millionaires = new QuizType
                {
                    CreationDate = creationDate,
                    Description = "In millionaires quiz you go answer by answer to the last question. You check your answer every step and the wrong one disqualifies you.",
                    Title = "millionaires",
                    IsDeleted = false
                };

                quizTypeService.Add(standard);
                quizTypeService.Add(millionaires);
            }

            if (colorService.GetList().Count() == 0)
            {
                string path = "Colors.json".ToApplicationPath();

                GeoJson<Color> geoJson = JsonConvert.DeserializeObject<GeoJson<Color>>(File.ReadAllText(path));

                foreach (var colorJson in geoJson.Features)
                {
                    Color color = colorJson.Properties;
                    color.CreationDate = creationDate;
                    colorService.Add(color);
                }
            }

            if (categoryService.GetList().Count() == 0)
            {
                string path = "Categories.json".ToApplicationPath();

                GeoJson<Category> geoJson = JsonConvert.DeserializeObject<GeoJson<Category>>(File.ReadAllText(path));

                foreach (var categoryJson in geoJson.Features)
                {
                    Category category = categoryJson.Properties;
                    category.CreationDate = creationDate;
                    categoryService.Add(category);
                }
            }

            //if (categoryService.GetQuestionCategoriesList().Count() == 0)
            //{
            //    string path = "CategoryQuestions.json".ToApplicationPath();

            //    GeoJson<CategoryQuestion> geoJson = JsonConvert.DeserializeObject<GeoJson<CategoryQuestion>>(File.ReadAllText(path));

            //    foreach (var categoryJson in geoJson.Features)
            //    {
            //        CategoryQuestion category = categoryJson.Properties;
            //        category.CreationDate = creationDate;
            //        categoryService.AddQuestionCategory(category);
            //    }
            //}

            //if (categoryService.GetChallengeCategoriesList().Count() == 0)
            //{
            //    string path = "ChallengeCategories.json".ToApplicationPath();

            //    GeoJson<ChallengeCategory> geoJson = JsonConvert.DeserializeObject<GeoJson<ChallengeCategory>>(File.ReadAllText(path));

            //    foreach (var categoryJson in geoJson.Features)
            //    {
            //        ChallengeCategory category = categoryJson.Properties;
            //        category.CreationDate = creationDate;
            //        categoryService.AddChallengeCategory(category);
            //    }
            //}

            if (questionService.GetList().Count() == 0)
            {
                List<Question> questions = new List<Question>();
                List<Answer> answers1 = new List<Answer>();
                List<Answer> answers2 = new List<Answer>();
                List<CategoryQuestion> categories = new List<CategoryQuestion>();
                CategoryQuestion category = new CategoryQuestion
                {
                    CreationDate = creationDate,
                    Category = categoryService.GetList().FirstOrDefault()
                };

                categories.Add(category);

                answers1.Add(new Answer
                {
                    CreationDate = creationDate,
                    Title = "Blue"
                });

                answers1.Add(new Answer
                {
                    CreationDate = creationDate,
                    Title = "Green"
                });

                answers1.Add(new Answer
                {
                    CreationDate = creationDate,
                    Title = "Red"
                });

                answers1.Add(new Answer
                {
                    CreationDate = creationDate,
                    Title = "Yellow"
                });

                questions.Add(new Question {
                    CreationDate = creationDate,
                    IsDeleted = false,
                    Title = "What is the color of the sky?",
                    Answers = answers1,
                    CategoryList = categories,
                    CorrectAnswerId = answers1[0].Id
                });

                answers2.Add(new Answer
                {
                    CreationDate = creationDate,
                    Title = "Stark"
                });

                answers2.Add(new Answer
                {
                    CreationDate = creationDate,
                    Title = "Martell"
                });

                answers2.Add(new Answer
                {
                    CreationDate = creationDate,
                    Title = "Lannister"
                });

                answers2.Add(new Answer
                {
                    CreationDate = creationDate,
                    Title = "Tyrell"
                });

                questions.Add(new Question
                {
                    CreationDate = creationDate,
                    IsDeleted = false,
                    Title = "In GoT series, John Snow 'formally' belongs to House ...?",
                    Answers = answers2,
                    CategoryList = categories,
                    CorrectAnswerId = answers2[0].Id
                });

                questionService.Add(questions[0]);
                questionService.Add(questions[1]);
            }

            //if (answerService.GetList().Count() == 0)
            //{
            //    string path = "Answers.json".ToApplicationPath();

            //    GeoJson<Answer> geoJson = JsonConvert.DeserializeObject<GeoJson<Answer>>(File.ReadAllText(path));

            //    foreach (var answerJson in geoJson.Features)
            //    {
            //        Answer answer = answerJson.Properties;
            //        answer.CreationDate = creationDate;
            //        answerService.AddToSeed(answer);
            //    }
            //}

            //if (answerService.GetCorrectAnswersList().Count() == 0)
            //{
            //    string path = "CorrectAnswers.json".ToApplicationPath();

            //    GeoJson<CorrectAnswer> geoJson = JsonConvert.DeserializeObject<GeoJson<CorrectAnswer>>(File.ReadAllText(path));

            //    foreach (var answerJson in geoJson.Features)
            //    {
            //        CorrectAnswer answer = answerJson.Properties;
            //        answer.CreationDate = creationDate;
            //        answerService.AddCorrectAnswer(answer);
            //    }
            //}

            if(challengeService.GetList().Count() == 0)
            {
                List<Question> questions = new List<Question>();
                List<Answer> answers1 = new List<Answer>();
                List<Answer> answers2 = new List<Answer>();
                List<ChallengeCategory> categories = new List<ChallengeCategory>();
                ChallengeCategory category = new ChallengeCategory
                {
                    CreationDate = creationDate,
                    Category = categoryService.GetList().FirstOrDefault()
                };

                categories.Add(category);

                Challenge challenge = new Challenge
                {
                    CreationDate = creationDate,
                    Color = colorService.GetList().FirstOrDefault(),
                    QuestionAmount = 10,
                    QuizType = quizTypeService.GetList().FirstOrDefault(),
                    Title = "Default Quiz",
                    CategoryList = categories
                };

                challengeService.Add(challenge);
            }
        }

        public static string ToApplicationPath(this string fileName)
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                                .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return Path.Combine(appRoot, fileName);
        }
    }
}
