using Microsoft.AspNetCore.Mvc;
using ServerCore.Models;
using ServerCore.Services.Contracts;
using System;
using System.Collections.Generic;
using ServerCore.DTOs;

namespace ServerCore.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly ICampSeasonService _seasonService;
        private readonly IUserService _userService;

        public AdminController(
            ICampSeasonService seasonService,
            IUserService userService)
        {
            _seasonService = seasonService;
            _userService = userService;
        }

        [HttpPost("season")]
        public IActionResult CreateSeason([FromBody] CampSeason season)
        {
            var seasonId = _seasonService.CreateSeason(season.Name, season.StartDate, season.EndDate);
            return CreatedAtAction(nameof(GetSeason), new { id = seasonId }, seasonId);
        }

        [HttpPost("season/{seasonId}/map")]
        public IActionResult AddChallengeMap(Guid seasonId, [FromBody] ChallengeMap map)
        {
            var mapId = _seasonService.CreateChallengeMap(seasonId, map.WeekNumber, map.Name);
            return CreatedAtAction(nameof(GetChallengeMap),
                new { seasonId, mapId }, mapId);
        }

        [HttpPost("map/{mapId}/challenge")]
        public IActionResult AddChallengeToMap(Guid mapId, [FromBody] Challenge challenge)
        {
            var result = _seasonService.AddLocationChallengeToMap(mapId, challenge);
            return result ? Ok() : BadRequest();
        }

        [HttpPost("user/{userId}/assign-challenge")]
        public IActionResult AssignPersonalChallenge(Guid userId, [FromBody] ChallengeDto challenge)
        {
            var result = _userService.AssignPersonalChallenge(userId, challenge);
            return result ? Ok() : BadRequest();
        }

        [HttpGet("season/{id}")]
        public IActionResult GetSeason(Guid id)
        {
            var season = _seasonService.GetSeason(id);
            return season != null ? Ok(season) : NotFound();
        }

        [HttpGet("season/{seasonId}/map/{mapId}")]
        public IActionResult GetChallengeMap(Guid seasonId, Guid mapId)
        {
            var map = _seasonService.GetChallengeMap(mapId);
            return map != null && map.CampSeasonId == seasonId ? Ok(map) : NotFound();
        }


        [HttpPost("season/{seasonId}/user/{userId}")]
        public IActionResult AddUserToSeason(Guid seasonId, Guid userId)
        {
            var result = _seasonService.AddUserToSeason(seasonId, userId);
            return result ? Ok() : BadRequest("Не удалось добавить пользователя в смену");
        }

    }
}