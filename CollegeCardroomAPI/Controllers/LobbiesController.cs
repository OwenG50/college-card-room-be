using CollegeCardroomAPI.Hubs;
using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CollegeCardroomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LobbiesController : ControllerBase
    {
        private readonly ILobbiesManager lobbiesManager;
        private readonly IHubContext<PokerRoomHub> pokerRoomHubContext;

        public LobbiesController(ILobbiesManager lobbiesManager, IHubContext<PokerRoomHub> pokerRoomHubContext)
        {
            this.lobbiesManager = lobbiesManager;
            this.pokerRoomHubContext = pokerRoomHubContext;

        }

        [HttpPost("{lobbyId}/start")]
        public async Task<IActionResult> StartGame(int lobbyId)
        {
            var lobby = lobbiesManager.GetLobby(lobbyId);
            if (lobby == null)
            {
                return NotFound();
            }

            lobbiesManager.SetLobbyStarted(lobbyId);

            foreach (var user in lobby.Users)
            {
                await pokerRoomHubContext.Groups.AddToGroupAsync(user.ConnectionId, lobbyId.ToString());
                await pokerRoomHubContext.Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage", "Welcome to the room!");
            }

            await pokerRoomHubContext.Clients.Group(lobbyId.ToString()).SendAsync("GameStarted", lobbyId);

            // Add logic for handling start logic here. Call a method from Game logic.

            return Ok();
        }

        [HttpPost("create")]
        public async Task <ActionResult<Lobby>> CreateLobby()
        {
            var lobby = lobbiesManager.CreateLobby();
            await pokerRoomHubContext.Clients.All.SendAsync("LobbyCreated", lobby);
            return Ok(lobby);
        }

        [HttpPost("{lobbyId}/join")]
        public async Task<IActionResult> JoinLobby(int lobbyId, [FromBody] User user)
        {
            lobbiesManager.AddUserToLobby(lobbyId, user);

            var lobby = lobbiesManager.GetLobby(lobbyId);
            if (lobby != null)
            {
                await pokerRoomHubContext.Groups.AddToGroupAsync(user.ConnectionId, lobbyId.ToString());
                await pokerRoomHubContext.Clients.All.SendAsync("LobbyUpdated", lobby);
            }

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
        public async Task<IActionResult> DeleteLobby(int lobbyId)
        {
            lobbiesManager.DeleteLobby(lobbyId);
            await pokerRoomHubContext.Clients.All.SendAsync("LobbyDeleted", lobbyId);
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