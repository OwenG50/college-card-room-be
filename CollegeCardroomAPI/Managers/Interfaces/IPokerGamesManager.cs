using CollegeCardroomAPI.Hubs;
using CollegeCardroomAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace CollegeCardroomAPI.Managers.Interfaces
{
    public interface IPokerGamesManager
    {
        PokerGame CreatePokerGame(int lobbyId, List<PokerPlayer> players);
        void SetGameSettings(Guid gameId, int smallBlindAmount, int bigBlindAmount, IHubContext<PokerRoomHub> hubContext);
        List<PokerGame> GetAllPokerGames();
        PokerGame? GetPokerGameByLobbyId(int lobbyId);
        PokerGame? GetPokerGame(Guid gameId);
        void DeletePokerGame(Guid gameId);
        void DeleteAllPokerGames();

    }
}