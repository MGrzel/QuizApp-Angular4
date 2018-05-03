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
                    Id = 1,
                    CreationDate = creationDate,
                    Description = "In standard quiz you have access to all of the questions immediately. Submiting the quiz means checking all the answers and ending the session.",
                    Title = "standard",
                    IsDeleted = false
                };

                QuizType millionaires = new QuizType
                {
                    Id = 2,
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

            if (categoryService.GetQuestionCategoriesList().Count() == 0)
            {
                string path = "CategoryQuestions.json".ToApplicationPath();

                GeoJson<CategoryQuestion> geoJson = JsonConvert.DeserializeObject<GeoJson<CategoryQuestion>>(File.ReadAllText(path));

                foreach (var categoryJson in geoJson.Features)
                {
                    CategoryQuestion category = categoryJson.Properties;
                    category.CreationDate = creationDate;
                    categoryService.AddQuestionCategory(category);
                }
            }

            if (categoryService.GetChallengeCategoriesList().Count() == 0)
            {
                string path = "ChallengeCategories.json".ToApplicationPath();

                GeoJson<ChallengeCategory> geoJson = JsonConvert.DeserializeObject<GeoJson<ChallengeCategory>>(File.ReadAllText(path));

                foreach (var categoryJson in geoJson.Features)
                {
                    ChallengeCategory category = categoryJson.Properties;
                    category.CreationDate = creationDate;
                    categoryService.AddChallengeCategory(category);
                }
            }

            if (questionService.GetList().Count() == 0)
            {
                string path = "Questions.json".ToApplicationPath();

                GeoJson<Question> geoJson = JsonConvert.DeserializeObject<GeoJson<Question>>(File.ReadAllText(path));

                foreach (var questionJson in geoJson.Features)
                {
                    Question question = questionJson.Properties;
                    question.CreationDate = creationDate;
                    questionService.AddToSeed(question);
                }
            }

            if (answerService.GetList().Count() == 0)
            {
                string path = "Answers.json".ToApplicationPath();

                GeoJson<Answer> geoJson = JsonConvert.DeserializeObject<GeoJson<Answer>>(File.ReadAllText(path));

                foreach (var answerJson in geoJson.Features)
                {
                    Answer answer = answerJson.Properties;
                    answer.CreationDate = creationDate;
                    answerService.AddToSeed(answer);
                }
            }

            if (answerService.GetCorrectAnswersList().Count() == 0)
            {
                string path = "CorrectAnswers.json".ToApplicationPath();

                GeoJson<CorrectAnswer> geoJson = JsonConvert.DeserializeObject<GeoJson<CorrectAnswer>>(File.ReadAllText(path));

                foreach (var answerJson in geoJson.Features)
                {
                    CorrectAnswer answer = answerJson.Properties;
                    answer.CreationDate = creationDate;
                    answerService.AddCorrectAnswer(answer);
                }
            }

            if(challengeService.GetList().Count() == 0)
            {
                string path = "Challenges.json".ToApplicationPath();

                GeoJson<Challenge> geoJson = JsonConvert.DeserializeObject<GeoJson<Challenge>>(File.ReadAllText(path));

                foreach(var challengeJson in geoJson.Features)
                {
                    Challenge challenge = challengeJson.Properties;
                    challenge.CreationDate = creationDate;
                    challengeService.AddToSeed(challenge);
                }
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
