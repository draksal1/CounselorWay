using Microsoft.AspNetCore.Mvc;
using ServerCore.Models;
using ServerCore.Services.Contracts;
using System;
using System.Collections.Generic;

namespace ServerCore.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IChallengeService _challengeService;

        public UserController(
            IUserService userService,
            IChallengeService challengeService)
        {
            _userService = userService;
            _challengeService = challengeService;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            var user = _userService.GetUser(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            var userId = _userService.RegisterUser(user.Name, user.Age);
            return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
        }

        [HttpGet("{userId}/challenges/available/{seasonId}")]
        public IActionResult GetAvailableChallenges(Guid userId, Guid seasonId)
        {
            var challenges = _challengeService.GetAvailableChallenges(userId, seasonId);
            return Ok(challenges);
        }

        [HttpPost("{userId}/complete/map/{mapId}/{challengeId}")]
        public IActionResult CompleteMapChallenge(Guid userId, Guid mapId, Guid challengeId)
        {
            var result = _challengeService.CompleteLocationChallenge(userId, mapId, challengeId);
            return result ? Ok() : BadRequest();
        }

        [HttpPost("{userId}/complete/personal/{challengeId}")]
        public IActionResult CompletePersonalChallenge(Guid userId, Guid challengeId)
        {
            var result = _challengeService.CompletePersonalChallenge(userId, challengeId);
            return result ? Ok() : BadRequest();
        }
    }
}