using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeCardroomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LobbiesController : ControllerBase
    {
        private readonly ILobbiesManager lobbiesManager;

        public LobbiesController(ILobbiesManager lobbiesManager)
        {
            this.lobbiesManager = lobbiesManager;
        }

        [HttpPost("create")]
        public ActionResult<Lobby> CreateLobby()
        {
            var lobby = lobbiesManager.CreateLobby();
            return Ok(lobby);
        }

        [HttpPost("{lobbyId}/join")]
        public IActionResult JoinLobby(int lobbyId, [FromBody] User user)
        {
            lobbiesManager.AddUserToLobby(lobbyId, user);
            return Ok();
        }

        [HttpGet("{lobbyId}")]
        public ActionResult<Lobby> GetLobby(int lobbyId)
        {
            var lobby = lobbiesManager.GetLobby(lobbyId);
            if (lobby == null)
            {
                return NotFound();
            }
            return Ok(lobby);
        }

        [HttpGet]
        public ActionResult<List<Lobby>> GetAllLobbies()
        {
            var lobbies = lobbiesManager.GetAllLobbies();
            return Ok(lobbies);
        }

        [HttpDelete("{lobbyId}/removeUser/{userId}")]
        public IActionResult RemoveUserFromLobby(int lobbyId, int userId)
        {
            lobbiesManager.RemoveUserFromLobby(lobbyId, userId);
            return Ok();
        }

        [HttpDelete("{lobbyId}")]
        public IActionResult DeleteLobby(int lobbyId)
        {
            lobbiesManager.DeleteLobby(lobbyId);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAllLobbies()
        {
            lobbiesManager.DeleteAllLobbies();
            return Ok();
        }
    }
}