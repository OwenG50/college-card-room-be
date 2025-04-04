namespace CollegeCardroomAPI.Models
{
    public class User
    {
        public required int UserId { get; set; }
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
