using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;

namespace CollegeCardroomAPI.Managers
{
    public class PokerGamesManager : IPokerGamesManager
    {
        private readonly IPokerGamesRepository pokerGamesRepository;

        public PokerGamesManager(IPokerGamesRepository pokerGamesRepository)
        {
            this.pokerGamesRepository = pokerGamesRepository;
        }

        public PokerGame CreatePokerGame(int lobbyId, List<PokerPlayer> players)
        {
            var url = "http://localhost:3000/poker-games/"; // Consider moving this to a config file
            var gameId = Guid.NewGuid();

            var pokerGame = new PokerGame
            {
                GameId = gameId,
                LobbyId = lobbyId,
                Players = players,
                IsGameStarted = false,
                CreatedAt = DateTime.UtcNow,
                GameUrl = url + gameId.ToString()
            };

            return pokerGamesRepository.CreatePokerGame(pokerGame);
        }

        public List<PokerGame> GetAllPokerGames()
        {
            return pokerGamesRepository.GetAllPokerGames();
        }

        public PokerGame? GetPokerGame(Guid gameId)
        {
            return pokerGamesRepository.GetPokerGame(gameId);
        }

        public PokerGame? GetPokerGameByLobbyId(int lobbyId)
        {
            return pokerGamesRepository.GetPokerGameByLobbyId(lobbyId);
        }

        public void DeletePokerGame(Guid gameId)
        { 
            pokerGamesRepository.DeletePokerGame(gameId);
        }

        public void DeleteAllPokerGames()
        { 
            pokerGamesRepository.DeleteAllPokerGames();
        }

    }
}