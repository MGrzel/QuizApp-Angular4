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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get/categories/{id?}")]
        public async Task<object> GetCategories(Guid? id)
        {
            if (id.HasValue)
            {
                return await _categoryService.GetById(id);
            }
            return await _categoryService.GetList();
        }

        [HttpGet("get/deleted/categories")]
        public async Task<List<Category>> GetDeletedCategories()
        {
            return await _categoryService.GetDeletedList();
        }

        [HttpPost("post/categories")]
        public async Task<object> AddCategory([FromBody]Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _categoryService.CheckIfExists(category) || !await _categoryService.Validate(category))
            {
                return BadRequest();
            }

            await _categoryService.Add(category);
            return category;
        }

        [HttpPut("put/categories/{id?}")]
        public async Task<object> UpdateCategory(Guid? id, [FromBody]Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.HasValue)
            {
                if (!await _categoryService.CheckIfExists(category) || !await _categoryService.Validate(category))
                {
                    return BadRequest();
                }

                await _categoryService.Update(category);
                return category;
            }
            return category;
        }

        [HttpPatch("patch/categories/{id}")]
        public async Task RestoreCategory(Guid id, [FromBody]Category category)
        {
            await _categoryService.Restore(category);
        }

        [HttpDelete("delete/categories/{id}")]
        public async Task DeleteCategory(Guid id)
        {
            await _categoryService.Delete(id);
        }
    }
}
