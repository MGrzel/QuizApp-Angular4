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
    public class ChallengeController : Controller
    {
        private readonly IChallengeService _challengeService;

        public ChallengeController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpGet("get/challenges/{id?}")]
        [AllowAnonymous]
        public async Task<object> GetChallenges(Guid? id)
        {
            if (id.HasValue)
            {
                return await _challengeService.GetById(id);
            }
            return await _challengeService.GetList();
        }

        [HttpGet("get/deleted/challenges/")]
        public async Task<List<Challenge>> GetDeletedChallenges()
        {
            return await _challengeService.GetDeletedList();
        }

        [HttpPost("post/challenges")]
        public async Task<object> AddChallenge([FromBody]Challenge challenge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _challengeService.CheckIfExists(challenge) || !await _challengeService.Validate(challenge))
            {
                return BadRequest();
            }

            await _challengeService.Add(challenge);
            return challenge;
        }

        [HttpPut("put/challenges/{id?}")]
        public async Task<object> UpdateChallenge(Guid? id, [FromBody]Challenge challenge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.HasValue)
            {
                if (!await _challengeService.CheckIfExists(challenge) || !await _challengeService.Validate(challenge))
                {
                    return BadRequest();
                }

                await _challengeService.Update(challenge);
                return challenge;
            }
            return challenge;
        }

        [HttpPatch("patch/challenges/{id}")]
        public async Task RestoreChallenge(Guid id, [FromBody]Challenge challenge)
        {
            await _challengeService.Restore(challenge);
        }

        [HttpDelete("delete/challenges/{id}")]
        public async Task DeleteChallenge(Guid id)
        {
            await _challengeService.Delete(id);
        }
    }
}
