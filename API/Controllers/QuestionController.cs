using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAppApi.Models;
using QuizAppApi.Services;

namespace QuizAppApi.Controllers
{
    [Route("quizapp")]
    [Authorize("Bearer")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionSerivce)
        {
            _questionService = questionSerivce;
        }

        [HttpGet("get/questions/{id?}")]
        public async Task<object> GetQuestions(Guid? id)
        {
            if (id.HasValue)
            {
                return await _questionService.GetById(id);
            }
            return await _questionService.GetList();
        }

        [HttpGet("get/deleted/questions/")]
        public async Task<object> GetDeletedQuestions()
        {
            return await _questionService.GetDeletedList();
        }

        [HttpGet("get/admin/questions/{id?}")]
        public async Task<object> GetQuestionsAsAdmin(Guid? id)
        {
            if (id.HasValue)
            {
                return await _questionService.GetById(id, true);
            }
            return await _questionService.GetList(true);
        }

        [HttpPost("post/questions")]
        public async Task<object> AddQuestion([FromBody]Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _questionService.CheckIfExists(question) || !await _questionService.Validate(question))
            {
                return BadRequest();
            }

            await _questionService.Add(question);
            return question;
        }

        [HttpPut("put/questions/{id?}")]
        public async Task<object> UpdateQuestion(Guid? id, [FromBody]Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.HasValue)
            {
                if (!await _questionService.CheckIfExists(question) || !await _questionService.Validate(question))
                {
                    return BadRequest();
                }

                await _questionService.Update(question);
                return question;
            }
            return question;
        }

        [HttpPatch("patch/questions/{id}")]
        public async Task RestoreQuestion(Guid id, [FromBody]Question question)
        {
            await _questionService.Restore(question);
        }

        [HttpDelete("delete/questions/{id}")]
        public async Task DeleteQuestion(Guid id)
        {
            await _questionService.Delete(id);
        }
    }
}
