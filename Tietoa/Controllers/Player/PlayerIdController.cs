﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.GetRequest;
using Services.Player;
using Tietoa.Domain.Models.Player;
using Tietoa.Domain.Models.Player.JsonClasses;


namespace Tietoa.Controllers.Player
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerIdController : ControllerBase
    {
        private readonly ILogger<PlayerIdController> _logger;
        private readonly IGetRequest _GetRequest;

        public PlayerIdController(ILogger<PlayerIdController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id) //8478007 = Elvis Merzlikins
        {
            if (id == 0)
                return BadRequest("Player id missing");

            var url = $"https://statsapi.web.nhl.com/api/v1/people/{id}";
            var playerResponse = JsonConvert.DeserializeObject<PlayerResponse>(_GetRequest.DownloadResponse(url).Result);
       
            // TODO: Bug for invalid player id

            PlayerDto playerDto = new PlayerDto
            {
                FirstName = playerResponse.people[0].firstName,
                LastName = playerResponse.people[0].lastName,
                Team = playerResponse.people[0].currentTeam.name,
                Position = playerResponse.people[0].primaryPosition.name,
                PlayerNumber = playerResponse.people[0].primaryNumber
            };
            return Ok(playerDto);
        }
    }
}
