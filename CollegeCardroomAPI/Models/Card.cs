namespace CollegeCardroomAPI.Models
{
    public class Card
    {
        public string Suit { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}
