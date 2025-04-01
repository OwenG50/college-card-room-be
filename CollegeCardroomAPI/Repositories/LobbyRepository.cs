using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CollegeCardroomAPI.Repositories
{
    public class LobbyRepository : ILobbyRepository
    {
        private readonly List<Lobby> lobbies;
        private readonly string filePath;
        private static int nextLobbyId = 1;

        public LobbyRepository(IHostEnvironment environment)
        {
            filePath = Path.Combine(environment.ContentRootPath, "Data", "Lobbies.json");
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                lobbies = JsonConvert.DeserializeObject<List<Lobby>>(jsonData) ?? new List<Lobby>();
                if (lobbies.Any())
                {
                    nextLobbyId = lobbies.Max(l => l.LobbyId) + 1;
                }
            }
            else
            {
                lobbies = new List<Lobby>();
            }
        }

        public Lobby CreateLobby()
        {
            var lobby = new Lobby
            {
                LobbyId = nextLobbyId++
            };
            lobbies.Add(lobby);
            SaveChanges();
            return lobby;
        }

        public Lobby GetLobby(int lobbyId)
        {
            return lobbies.FirstOrDefault(l => l.LobbyId == lobbyId);
        }

        public void AddUserToLobby(int lobbyId, User user)
        {
            var lobby = GetLobby(lobbyId);
            if (lobby != null)
            {
                lobby.Users.Add(user);
                SaveChanges();
            }
        }

        public List<Lobby> GetAllLobbies()
        {
            return lobbies;
        }

        private void SaveChanges()
        {
            var jsonData = JsonConvert.SerializeObject(lobbies, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
    }
}