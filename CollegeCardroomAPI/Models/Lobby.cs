namespace CollegeCardroomAPI.Models
{
    public class Lobby
    {
        public required int LobbyId { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public bool isStarted { get; set; } = false;
    }
}