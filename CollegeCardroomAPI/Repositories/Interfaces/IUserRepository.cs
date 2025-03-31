using CollegeCardroomAPI.Models;
using System.Collections.Generic;

namespace CollegeCardroomAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        void RemoveUser(int userId);
    }
}