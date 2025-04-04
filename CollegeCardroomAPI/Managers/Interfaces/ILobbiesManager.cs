using CollegeCardroomAPI.Models;

namespace CollegeCardroomAPI.Managers.Interfaces
{
    public interface ILobbiesManager
    {
        Lobby CreateLobby();
        Lobby GetLobby(int lobbyId);
        void AddUserToLobby(int lobbyId, User user);
        List<Lobby> GetAllLobbies();
        void RemoveUserFromLobby(int lobbyId, int userId);
        void DeleteLobby(int lobbyId);
        void DeleteAllLobbies();
    }
}