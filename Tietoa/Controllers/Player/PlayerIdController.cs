using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.NHL.NhlRequest;
using Tietoa.Domain.Models.Player;
using static Tietoa.Domain.Models.Player.PlayerDto;

namespace Tietoa.Controllers.Player
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerIdController : ControllerBase
    {
        private readonly ILogger<PlayerIdController> _logger;
        private readonly INhlRequest _NhlRequest;

        public PlayerIdController(ILogger<PlayerIdController> logger, INhlRequest nhlRequest)
        {
            _logger = logger;
            _NhlRequest = nhlRequest;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id) //8478007 = Elvis Merzlikins
        {
            if (id == 0)
                return BadRequest("Player id missing");

            var url = $"https://statsapi.web.nhl.com/api/v1/people/{id}";
            var response = await _NhlRequest.NHLGetResponse(url);
            var root = JsonConvert.DeserializeObject<Root>(response);


            if (root?.people == null)
                return NotFound(); 

            // TODO: Bug for invalid player id

            List<PlayerDto> playerDto = new List<PlayerDto>();
            {
                playerDto.Add(new PlayerDto
                {
                    FirstName = root.people[0].firstName,
                    LastName = root.people[0].lastName,
                    Team = root.people[0].currentTeam.name,
                    Position = root.people[0].primaryPosition.name,
                    PlayerNumber = root.people[0].primaryNumber
                }) ;
            }
            return Ok(playerDto);
        }
    }
}
