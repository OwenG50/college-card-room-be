using CollegeCardroomAPI.Models;
using System.Collections.Generic;

namespace CollegeCardroomAPI.Repositories.Interfaces
{
    public interface ILobbiesRepository
    {
        Lobby CreateLobby();
        Lobby GetLobby(int lobbyId);
        void AddUserToLobby(int lobbyId, User user);
        List<Lobby> GetAllLobbies();
        void RemoveUserFromLobby(int lobbyId, int userId);
        void DeleteLobby(int lobbyId);
        void DeleteAllLobbies();
        void SetLobbyStarted(int lobbyId);
    }
}