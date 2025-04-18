using CollegeCardroomAPI.Models;

namespace CollegeCardroomAPI.Managers.Interfaces
{
    public interface IPokerGamesManager
    {
        PokerGame CreatePokerGame(int lobbyId, List<PokerPlayer> players);
        PokerGame? GetPokerGame(Guid gameId);
    }
}