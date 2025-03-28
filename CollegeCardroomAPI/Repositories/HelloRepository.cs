using CollegeCardroomAPI.Repositories.Interfaces;

namespace CollegeCardroomAPI.Repositories
{
    public class HelloRepository : IHelloRepository
    {
        public string GetHelloMessage()
        {
            return "Hello from the Repository!";
        }

    }
}
