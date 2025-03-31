using CollegeCardroomAPI.Models;

namespace CollegeCardroomAPI.Managers.Interfaces
{
    public interface ILobbyManager
    {
        Lobby CreateLobby();
        Lobby GetLobby(int lobbyId);
        void AddUserToLobby(int lobbyId, User user);
    }
}