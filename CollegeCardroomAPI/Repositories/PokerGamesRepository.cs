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

        private void SaveChanges()
        {
            var jsonData = JsonConvert.SerializeObject(pokerGames, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
    }
}