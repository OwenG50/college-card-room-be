using CollegeCardroomAPI.Models;

namespace CollegeCardroomAPI.Repositories.Interfaces
{
    public interface IPokerGamesRepository
    {
        PokerGame CreatePokerGame(PokerGame pokerGame);
        PokerGame? GetPokerGame(Guid gameId);

    }
}