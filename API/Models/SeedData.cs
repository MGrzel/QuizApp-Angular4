using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public static void Initialize(QuizAppDb context, ILoggerFactory loggerFactory)
        {
            QuizAppDb _context = context;

            DateTime creationDate = DateTime.Now;

            string pathToJson = "SeedDataJson/SeedData.json".ToApplicationPath();

            ILogger logger = loggerFactory.CreateLogger("SeedaDataLogger");

            if (!_context.QuizTypes.Any())
            {
                using (StreamReader file = File.OpenText(pathToJson))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    JObject jObject = (JObject)serializer.Deserialize(file, typeof(JObject));

                    List<QuizType> quizTypes = jObject["QuizTypes"].ToObject<List<QuizType>>();

                    foreach (QuizType quizType in quizTypes)
                    {
                        quizType.CreationDate = creationDate;
                    }

                    _context.QuizTypes.AddRange(quizTypes);
                    _context.SaveChanges();
                }
            }

            if (!_context.Categories.Any())
            {
                using (StreamReader file = File.OpenText(pathToJson))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    JObject jObject = (JObject)serializer.Deserialize(file, typeof(JObject));

                    List<Category> categories = jObject["Categories"].ToObject<List<Category>>();

                    foreach (Category category in categories)
                    {
                        category.CreationDate = creationDate;
                    }

                    _context.Categories.AddRange(categories);
                    _context.SaveChanges();
                }
            }

            if (!_context.Colors.Any())
            {
                using (StreamReader file = File.OpenText(pathToJson))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    JObject jObject = (JObject)serializer.Deserialize(file, typeof(JObject));

                    List<Color> colors = jObject["Colors"].ToObject<List<Color>>();

                    foreach (Color color in colors)
                    {
                        color.CreationDate = creationDate;
                    }

                    _context.Colors.AddRange(colors);
                    _context.SaveChanges();
                }
            }

            if (!_context.Questions.Any())
            {
                using (StreamReader file = File.OpenText(pathToJson))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    JObject jObject = (JObject)serializer.Deserialize(file, typeof(JObject));

                    List<Question> questions = jObject["Questions"].ToObject<List<Question>>();

                    foreach (Question question in questions)
                    {
                        question.CreationDate = creationDate;
                        question.CategoryList = new List<CategoryQuestion>
                        {
                            new CategoryQuestion
                            {
                                CreationDate = creationDate,
                                Category = _context.Categories.FirstOrDefault()
                            }
                        };
                    }

                    _context.Questions.AddRange(questions);
                    _context.SaveChanges();
                }
            }


            if (!_context.Challenges.Any())
            {
                using (StreamReader file = File.OpenText(pathToJson))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    JObject jObject = (JObject)serializer.Deserialize(file, typeof(JObject));

                    List<Challenge> challenges = jObject["Challenges"].ToObject<List<Challenge>>();

                    foreach (Challenge challenge in challenges)
                    {
                        challenge.CreationDate = creationDate;
                        challenge.CategoryList = new List<ChallengeCategory>
                        {
                            new ChallengeCategory
                            {
                                CreationDate = creationDate,
                                Category = _context.Categories.FirstOrDefault()
                            }
                        };
                        challenge.Color = _context.Colors.Take(3).Last();
                        challenge.QuizType = _context.QuizTypes.FirstOrDefault();
                    }

                    _context.Challenges.AddRange(challenges);
                    _context.SaveChanges();
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
