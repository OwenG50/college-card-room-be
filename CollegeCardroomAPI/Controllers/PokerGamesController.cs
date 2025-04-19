using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeCardroomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokerGamesController : ControllerBase
    {
        private readonly IPokerGamesManager pokerGamesManager;

        public PokerGamesController(IPokerGamesManager pokerGamesManager)
        {
            this.pokerGamesManager = pokerGamesManager;
        }

        [HttpGet]
        public IActionResult GetAllPokerGames()
        {
            var pokerGames = pokerGamesManager.GetAllPokerGames();
            if (pokerGames == null || pokerGames.Count == 0)
            {
                return NotFound("No poker games found.");
            }
            return Ok(pokerGames);
        }

        [HttpGet("{gameId:guid}")]
        public ActionResult<PokerGame> GetPokerGame(Guid gameId)
        {
            var pokerGame = pokerGamesManager.GetPokerGame(gameId);
            if (pokerGame == null)
            {
                return NotFound($"Poker game with ID {gameId} not found.");
            }

            return Ok(pokerGame);
        }

        [HttpGet("lobby/{lobbyId:int}")]
        public ActionResult<PokerGame> GetPokerGameByLobbyId(int lobbyId)
        {
            var pokerGame = pokerGamesManager.GetPokerGameByLobbyId(lobbyId);
            if (pokerGame == null)
            {
                return NotFound($"Poker game with Lobby ID {lobbyId} not found.");
            }
            return Ok(pokerGame);
        }

        [HttpDelete("{gameId:guid}")]
        public IActionResult DeletePokerGame(Guid gameId)
        {
            var pokerGame = pokerGamesManager.GetPokerGame(gameId);
            if (pokerGame == null)
            {
                return NotFound($"Poker game with ID {gameId} not found.");
            }

            pokerGamesManager.DeletePokerGame(gameId);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteAllPokerGames()
        {
            pokerGamesManager.DeleteAllPokerGames();
            return NoContent();
        }
    }
}