using System.Collections.Generic;
using DeckOfCardsLibrary;

namespace CollegeCardroomAPI.Models
{
    public class PokerGame
    {
        public required Guid GameId { get; set; }
        public required int LobbyId { get; set; }
        public List<PokerPlayer> Players { get; set; } = new List<PokerPlayer>();
        public PokerPlayer? BigBlind { get; set; }
        public PokerPlayer? SmallBlind { get; set; }
        public PokerPlayer? Dealer { get; set; }
        public Deck Deck { get; set; } = Deck.get();
        public int BigBlindAmount { get; set; }
        public int SmallBlindAmount { get; set; }
        public int Pot { get; set; }
        public bool IsGameStarted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string GameUrl { get; set; } = string.Empty;
    }
}