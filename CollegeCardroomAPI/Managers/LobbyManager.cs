using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;

namespace CollegeCardroomAPI.Managers
{
    public class LobbyManager : ILobbyManager
    {
        private readonly ILobbyRepository lobbyRepository;

        public LobbyManager(ILobbyRepository lobbyRepository)
        {
            this.lobbyRepository = lobbyRepository;
        }

        public Lobby CreateLobby()
        {
            return lobbyRepository.CreateLobby();
        }

        public Lobby GetLobby(int lobbyId)
        {
            return lobbyRepository.GetLobby(lobbyId);
        }

        public void AddUserToLobby(int lobbyId, User user)
        {
            lobbyRepository.AddUserToLobby(lobbyId, user);
        }

        public List<Lobby> GetAllLobbies()
        {
            return lobbyRepository.GetAllLobbies();
        }
    }
}