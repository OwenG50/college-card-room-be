using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeCardroomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LobbyController : ControllerBase
    {
        private readonly ILobbyManager lobbyManager;

        public LobbyController(ILobbyManager lobbyManager)
        {
            this.lobbyManager = lobbyManager;
        }

        [HttpPost("create")]
        public ActionResult<Lobby> CreateLobby()
        {
            var lobby = lobbyManager.CreateLobby();
            return Ok(lobby);
        }

        [HttpPost("{lobbyId}/join")]
        public IActionResult JoinLobby(int lobbyId, [FromBody] User user)
        {
            lobbyManager.AddUserToLobby(lobbyId, user);
            return Ok();
        }

        [HttpGet("{lobbyId}")]
        public ActionResult<Lobby> GetLobby(int lobbyId)
        {
            var lobby = lobbyManager.GetLobby(lobbyId);
            if (lobby == null)
            {
                return NotFound();
            }
            return Ok(lobby);
        }

        [HttpGet]
        public ActionResult<List<Lobby>> GetAllLobbies()
        {
            var lobbies = lobbyManager.GetAllLobbies();
            return Ok(lobbies);
        }
    }
}