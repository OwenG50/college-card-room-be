using System.Collections.Generic;

namespace CollegeCardroomAPI.Models
{
    public class PokerGame
    {
        public required int GameId { get; set; }
        public List<PokerPlayer> Players { get; set; } = new List<PokerPlayer>();
        public PokerPlayer? BigBlind { get; set; }
        public PokerPlayer? SmallBlind { get; set; }
        public PokerPlayer? Dealer { get; set; }
        public List<Card> Deck { get; set; } = new List<Card>();
        public int BigBlindAmount { get; set; }
        public int SmallBlindAmount { get; set; }
        public int Pot { get; set; }
    }
}