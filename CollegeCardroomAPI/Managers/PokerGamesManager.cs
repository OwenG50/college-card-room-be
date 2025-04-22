using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;
using DeckOfCardsLibrary;

namespace CollegeCardroomAPI.Managers
{
    public class PokerGamesManager : IPokerGamesManager
    {
        private readonly IPokerGamesRepository pokerGamesRepository;
        private string gameUrl = "http://localhost:3000/poker-games/"; // Consider moving this to a config file

        public PokerGamesManager(IPokerGamesRepository pokerGamesRepository)
        {
            this.pokerGamesRepository = pokerGamesRepository;
        }

        public PokerGame CreatePokerGame(int lobbyId, List<PokerPlayer> players)
        {
            var gameId = Guid.NewGuid();

            var pokerGame = new PokerGame
            {
                GameId = gameId,
                LobbyId = lobbyId,
                Players = players,
                IsGameStarted = false,
                CreatedAt = DateTime.UtcNow,
                GameUrl = gameUrl + gameId.ToString(),
                Deck = Deck.get(),
            };

            return pokerGamesRepository.CreatePokerGame(pokerGame);
        }

        public void SetGameSettings(Guid gameId, int smallBlindAmount, int bigBlindAmount)
        {
            var pokerGame = pokerGamesRepository.GetPokerGame(gameId);

            if (pokerGame == null)
            {
                throw new ArgumentException("Poker game not found.");
            }

            pokerGame.SmallBlindAmount = smallBlindAmount;
            pokerGame.BigBlindAmount = bigBlindAmount;

            // Randomly select a dealer
            if (pokerGame.Players.Count > 0)
            {
                var random = new Random();
                var dealerIndex = random.Next(pokerGame.Players.Count);
                pokerGame.Dealer = pokerGame.Players[dealerIndex];
            }
            else
            {
                throw new InvalidOperationException("No players available to select a dealer.");
            }

            // Save the updated game settings
            pokerGamesRepository.UpdatePokerGame(pokerGame);
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