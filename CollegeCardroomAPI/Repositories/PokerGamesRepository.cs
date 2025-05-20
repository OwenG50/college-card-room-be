using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;
using Newtonsoft.Json;

namespace CollegeCardroomAPI.Repositories
{
    public class PokerGamesRepository : IPokerGamesRepository
    {
        private readonly List<PokerGame> pokerGames;
        private readonly string filePath;

        public PokerGamesRepository(IHostEnvironment environment)
        {
            filePath = Path.Combine(environment.ContentRootPath, "Data", "PokerGames.json");
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                pokerGames = JsonConvert.DeserializeObject<List<PokerGame>>(jsonData) ?? new List<PokerGame>();
            }
            else
            {
                pokerGames = new List<PokerGame>();
            }
        }

        public PokerGame CreatePokerGame(PokerGame pokerGame)
        {
            pokerGames.Add(pokerGame);
            SaveChanges();
            return pokerGame;
        }

        public List<PokerGame> GetAllPokerGames()
        {
            return pokerGames;
        }

        public PokerGame? GetPokerGame(Guid gameId)
        {
            return pokerGames.FirstOrDefault(pg => pg.GameId == gameId);
        }

        public PokerGame? GetPokerGameByLobbyId(int lobbyId)
        {
            return pokerGames.FirstOrDefault(game => game.LobbyId == lobbyId);
        }

        public void DeletePokerGame(Guid gameId)
        {
            var pokerGame = GetPokerGame(gameId);
            if (pokerGame != null)
            {
                pokerGames.Remove(pokerGame);
                SaveChanges();
            }
        }

        public void DeleteAllPokerGames()
        {
            pokerGames.Clear();
            SaveChanges();
        }

        public void UpdatePokerGame(PokerGame updatedGame)
        {
            var existingGame = GetPokerGame(updatedGame.GameId);

            if (existingGame == null)
            {
                throw new ArgumentException($"Poker game with ID {updatedGame.GameId} not found.");
            }

            // Update existing players
            foreach (var updatedPlayer in updatedGame.Players)
            {
                var existingPlayer = existingGame.Players.FirstOrDefault(p => p.UserId == updatedPlayer.UserId);

                if (existingPlayer != null)
                {
                    existingPlayer.ChipCount = updatedPlayer.ChipCount;
                    existingPlayer.SeatNumber = updatedPlayer.SeatNumber;
                    existingPlayer.CurrentHand = updatedPlayer.CurrentHand;
                    existingPlayer.IsFolded = updatedPlayer.IsFolded;
                    existingPlayer.IsBigBlind = updatedPlayer.IsBigBlind;
                    existingPlayer.IsSmallBlind = updatedPlayer.IsSmallBlind;
                    existingPlayer.ActionAmount = updatedPlayer.ActionAmount;
                    existingPlayer.ConnectionId = updatedPlayer.ConnectionId;
                }
                else
                {
                    // Add new players if they don't already exist
                    existingGame.Players.Add(new PokerPlayer
                    {
                        UserId = updatedPlayer.UserId,
                        UserName = updatedPlayer.UserName,
                        ChipCount = updatedPlayer.ChipCount,
                        SeatNumber = updatedPlayer.SeatNumber,
                        CurrentHand = updatedPlayer.CurrentHand,
                        IsFolded = updatedPlayer.IsFolded,
                        IsBigBlind = updatedPlayer.IsBigBlind,
                        IsSmallBlind = updatedPlayer.IsSmallBlind,
                        ActionAmount = updatedPlayer.ActionAmount,
                        ConnectionId = updatedPlayer.ConnectionId
                    });
                }
            }

            // Update other game properties
            existingGame.BigBlind = updatedGame.BigBlind;
            existingGame.SmallBlind = updatedGame.SmallBlind;
            existingGame.Dealer = updatedGame.Dealer;
            existingGame.Deck = updatedGame.Deck;
            existingGame.BigBlindAmount = updatedGame.BigBlindAmount;
            existingGame.SmallBlindAmount = updatedGame.SmallBlindAmount;
            existingGame.Pot = updatedGame.Pot;
            existingGame.IsGameStarted = updatedGame.IsGameStarted;

            SaveChanges();
        }

        private void SaveChanges()
        {
            var jsonData = JsonConvert.SerializeObject(pokerGames, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
    }
}