using CollegeCardroomAPI.Hubs;
using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;
using DeckOfCardsLibrary;
using Microsoft.AspNetCore.SignalR;

namespace CollegeCardroomAPI.Managers
{
    public class PokerGamesManager(IPokerGamesRepository pokerGamesRepository) : IPokerGamesManager
    {
        private readonly string gameUrl = "http://localhost:3000/poker-games/"; // Consider moving this to a config file
        private readonly IPokerGamesRepository pokerGamesRepository = pokerGamesRepository;

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

        public void SetGameSettings(Guid gameId, int smallBlindAmount, int bigBlindAmount, IHubContext<PokerRoomHub> hubContext)
        {
            var pokerGame = pokerGamesRepository.GetPokerGame(gameId) ?? throw new ArgumentException("Poker game not found.");
            pokerGame.SmallBlindAmount = smallBlindAmount;
            pokerGame.BigBlindAmount = bigBlindAmount;

            // Save the updated game settings
            pokerGamesRepository.UpdatePokerGame(pokerGame);

            // Broadcast update
            hubContext.Clients.Group(pokerGame.GameId.ToString()).SendAsync("UpdateGameSettings", pokerGame);
        }

        public void SelectInitialDealerAndBlinds(Guid gameId, IHubContext<PokerRoomHub> hubContext)
        {
            var pokerGame = pokerGamesRepository.GetPokerGame(gameId) ?? throw new ArgumentException("Poker game not found.");
            var players = pokerGame.Players;
            if (players == null || players.Count == 0)
            {
                throw new InvalidOperationException("No players in the game.");
            }

            // Reset all roles
            foreach (var player in players)
            {
                player.IsBigBlind = false;
                player.IsSmallBlind = false;
            }

            // Select dealer randomly
            var random = new Random();
            int dealerIndex = random.Next(players.Count);
            var dealer = players[dealerIndex];
            pokerGame.Dealer = dealer;

            // Small blind is next player (circular)
            int smallBlindIndex = (dealerIndex + 1) % players.Count;
            var smallBlind = players[smallBlindIndex];
            pokerGame.SmallBlind = smallBlind;
            smallBlind.IsSmallBlind = true;

            // Big blind logic
            if (players.Count > 2)
            {
                int bigBlindIndex = (dealerIndex + 2) % players.Count;
                var bigBlind = players[bigBlindIndex];
                pokerGame.BigBlind = bigBlind;
                bigBlind.IsBigBlind = true;
            }
            else
            {
                // Dealer is also the big blind
                pokerGame.BigBlind = dealer;
                dealer.IsBigBlind = true;
            }

            // Save changes
            pokerGamesRepository.UpdatePokerGame(pokerGame);

            // Broadcast update
            hubContext.Clients.Group(pokerGame.GameId.ToString()).SendAsync("UpdateGameSettings", pokerGame);
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

        public void StartGame(Guid gameId, IHubContext<PokerRoomHub> hubContext)
        {
            var pokerGame = pokerGamesRepository.GetPokerGame(gameId) ?? throw new ArgumentException("Poker game not found.");
            pokerGame.IsGameStarted = true;
            pokerGamesRepository.UpdatePokerGame(pokerGame);
        }

    }
}