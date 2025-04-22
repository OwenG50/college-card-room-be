using CollegeCardroomAPI.Models;

namespace CollegeCardroomAPI.Repositories.Interfaces
{
    public interface IPokerGamesRepository
    {
        PokerGame CreatePokerGame(PokerGame pokerGame);
        List<PokerGame> GetAllPokerGames();
        PokerGame? GetPokerGame(Guid gameId);
        PokerGame? GetPokerGameByLobbyId(int lobbyId);
        void DeletePokerGame(Guid gameId);
        void DeleteAllPokerGames();
        void UpdatePokerGame(PokerGame updatedGame);

    }
}