﻿using CollegeCardroomAPI.Models;
using System.Collections.Generic;

namespace CollegeCardroomAPI.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        void RemoveUser(int userId);
        User GetUser(int userId);
        void UpdateUser(User user);
    }
}