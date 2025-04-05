using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;

namespace CollegeCardroomAPI.Managers
{
    public class LobbiesManager : ILobbiesManager
    {
        private readonly ILobbiesRepository lobbiesRepository;

        public LobbiesManager(ILobbiesRepository lobbiesRepository)
        {
            this.lobbiesRepository = lobbiesRepository;
        }

        public Lobby CreateLobby()
        {
            return lobbiesRepository.CreateLobby();
        }

        public Lobby GetLobby(int lobbyId)
        {
            return lobbiesRepository.GetLobby(lobbyId);
        }

        public void AddUserToLobby(int lobbyId, User user)
        {
            lobbiesRepository.AddUserToLobby(lobbyId, user);
        }

        public List<Lobby> GetAllLobbies()
        {
            return lobbiesRepository.GetAllLobbies();
        }

        public void RemoveUserFromLobby(int lobbyId, int userId)
        {
            lobbiesRepository.RemoveUserFromLobby(lobbyId, userId);
        }

        public void DeleteLobby(int lobbyId)
        {
            lobbiesRepository.DeleteLobby(lobbyId);
        }

        public void DeleteAllLobbies()
        {
            var lobbies = lobbiesRepository.GetAllLobbies();
            var lobbyIds = lobbies.Select(lobby => lobby.LobbyId).ToList();
            foreach (var lobbyId in lobbyIds)
            {
                lobbiesRepository.DeleteLobby(lobbyId);
            }
        }

        public void SetLobbyStarted(int lobbyId)
        {
            lobbiesRepository.SetLobbyStarted(lobbyId);
        }
    }
}