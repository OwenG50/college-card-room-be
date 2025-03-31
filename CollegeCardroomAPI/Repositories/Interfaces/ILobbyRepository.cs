using CollegeCardroomAPI.Models;
using System.Collections.Generic;

namespace CollegeCardroomAPI.Repositories.Interfaces
{
    public interface ILobbyRepository
    {
        Lobby CreateLobby();
        Lobby GetLobby(int lobbyId);
        void AddUserToLobby(int lobbyId, User user);
    }
}