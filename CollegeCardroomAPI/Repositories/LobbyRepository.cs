using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CollegeCardroomAPI.Repositories
{
    public class LobbyRepository : ILobbyRepository
    {
        private static readonly List<Lobby> Lobbies = new();
        private static int nextLobbyId = 1;

        public Lobby CreateLobby()
        {
            var lobby = new Lobby
            {
                LobbyId = nextLobbyId++
            };
            Lobbies.Add(lobby);
            return lobby;
        }

        public Lobby GetLobby(int lobbyId)
        {
            return Lobbies.FirstOrDefault(l => l.LobbyId == lobbyId);
        }

        public void AddUserToLobby(int lobbyId, User user)
        {
            var lobby = GetLobby(lobbyId);
            if (lobby != null)
            {
                lobby.Users.Add(user);
            }
        }
    }
}