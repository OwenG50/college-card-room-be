using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CollegeCardroomAPI.Repositories
{
    public class LobbiesRepository : ILobbiesRepository
    {
        private readonly List<Lobby> lobbies;
        private readonly string filePath;
        private static int nextLobbyId = 1;

        public LobbiesRepository(IHostEnvironment environment)
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

        public void DeleteLobby(int lobbyId)
        {
            var lobby = GetLobby(lobbyId);
            if (lobby != null)
            {
                lobbies.Remove(lobby);
                SaveChanges();
            }
        }
        
        public void DeleteAllLobbies()
        {
            lobbies.Clear();
            SaveChanges();
        }

        public void RemoveUserFromLobby(int lobbyId, int userId)
        {
            var lobby = GetLobby(lobbyId);
            if (lobby != null)
            {
                var user = lobby.Users.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    lobby.Users.Remove(user);
                    SaveChanges();
                }
            }
        }

        private void SaveChanges()
        {
            var jsonData = JsonConvert.SerializeObject(lobbies, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }


    }
}