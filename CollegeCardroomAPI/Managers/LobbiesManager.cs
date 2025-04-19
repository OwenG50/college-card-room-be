using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;

namespace CollegeCardroomAPI.Managers
{
    public class LobbiesManager : ILobbiesManager
    {
        private readonly ILobbiesRepository lobbiesRepository;
        private readonly IPokerGamesManager pokerGamesManager;

        public LobbiesManager(ILobbiesRepository lobbiesRepository, IPokerGamesManager pokerGamesManager)
        {
            this.lobbiesRepository = lobbiesRepository;

            this.pokerGamesManager = pokerGamesManager;

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

        public void HandleLobbyStart(int lobbyId)
        {
            var lobby = GetLobby(lobbyId);
            if (lobby == null || lobby.Users.Count == 0)
            {
                throw new InvalidOperationException("Lobby not found or no users in the lobby.");
            }

            var pokerPlayers = ConvertUsersToPokerPlayers(lobby.Users);
            var pokerGame = pokerGamesManager.CreatePokerGame(lobbyId, pokerPlayers);

            // Additional game start logic can be added here
        }

        private List<PokerPlayer> ConvertUsersToPokerPlayers(List<User> users)
        {
            return users.Select(user => new PokerPlayer
            {
                UserId = user.UserId,
                UserName = user.UserName,
                ConnectionId = user.ConnectionId
            }).ToList();
        }

    }
}