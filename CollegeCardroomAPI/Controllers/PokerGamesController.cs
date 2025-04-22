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

        [HttpPost("{gameId:guid}/settings")]
        public IActionResult SetGameSettings(Guid gameId, [FromBody] GameSettingsRequest request)
        {
            try
            {
                pokerGamesManager.SetGameSettings(gameId, request.SmallBlindAmount, request.BigBlindAmount);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class GameSettingsRequest
    {
        // To update more settings in the future, we can add more properties here and into the managers method paramaters
        public int SmallBlindAmount { get; set; }
        public int BigBlindAmount { get; set; }
    }

}