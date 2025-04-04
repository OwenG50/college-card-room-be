using CollegeCardroomAPI.Models;
using CollegeCardroomAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;

namespace CollegeCardroomAPI.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly List<User> users;
        private readonly string filePath;

        public UsersRepository(IHostEnvironment environment)
        {
            filePath = Path.Combine(environment.ContentRootPath, "Data", "Users.json");
            var jsonData = File.ReadAllText(filePath);
            users = JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public User GetUser(int userId)
        {
            return users.FirstOrDefault(u => u.UserId == userId);
        }

        public void AddUser(User user)
        {
            users.Add(user);
            SaveChanges();
        }

        public void RemoveUser(int userId)
        {
            var user = users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                users.Remove(user);
                SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            var existingUser = users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
    }
}
