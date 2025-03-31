namespace CollegeCardroomAPI.Models
{
    public class Lobby
    {
        public int LobbyId { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}