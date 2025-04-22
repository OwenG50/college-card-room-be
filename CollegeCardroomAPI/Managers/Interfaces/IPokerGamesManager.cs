using CollegeCardroomAPI.Models;

namespace CollegeCardroomAPI.Managers.Interfaces
{
    public interface IPokerGamesManager
    {
        PokerGame CreatePokerGame(int lobbyId, List<PokerPlayer> players);
        List<PokerGame> GetAllPokerGames();
        PokerGame? GetPokerGameByLobbyId(int lobbyId);
        PokerGame? GetPokerGame(Guid gameId);
        void DeletePokerGame(Guid gameId);
        void DeleteAllPokerGames();
        void SetGameSettings(Guid gameId, int smallBlindAmount, int bigBlindAmount);

    }
}