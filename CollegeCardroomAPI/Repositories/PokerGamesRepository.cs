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

        public PokerGame? GetPokerGame(Guid gameId)
        {
            return pokerGames.FirstOrDefault(pg => pg.GameId == gameId);
        }

        private void SaveChanges()
        {
            var jsonData = JsonConvert.SerializeObject(pokerGames, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
    }
}