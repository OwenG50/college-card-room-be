namespace CollegeCardroomAPI.Models
{
    public class PokerPlayer
    {
        public required int UserId { get; set; }
        public required string UserName { get; set; }
        public int ChipCount { get; set; }
        public int SeatNumber { get; set; }
        public List<string> CurrentHand { get; set; } = new List<string>();
        public bool IsFolded { get; set; }
        public bool IsBigBlind { get; set; }
        public bool IsSmallBlind { get; set; }
        public List<string> Actions { get; set; } = new List<string> { "bet", "call", "fold", "raise", "check" };
    }
}
