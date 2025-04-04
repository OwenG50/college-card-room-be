using CollegeCardroomAPI.Models;
using System.Collections.Generic;

namespace CollegeCardroomAPI.Managers.Interfaces
{
    public interface IUsersManager
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        void RemoveUser(int userId);
        User GetUser(int userId);
        User UpdateUser(User user);
    }
}