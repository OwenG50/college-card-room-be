using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;

namespace CollegeCardroomAPI.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public User GetUser(int userId)
        {
            return userRepository.GetUser(userId);
        }

        public void AddUser(User user)
        {
            userRepository.AddUser(user);
        }

        public void RemoveUser(int userId)
        {
            userRepository.RemoveUser(userId);
        }
    }
}