using System;
using MongoDB.Driver;
using CalendarApp.Models;
using System.Collections.Generic;
using CalendarApp.Data;
using System.Linq;

namespace CalendarApp.Services
{
    public class UserService
    {
        private readonly MongoDbContext _dbContext;

        public UserService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserDTO> GetAllUsers()
        {
            var users = _dbContext.Users.Find(u => true).ToList();
            return users.Select(u => new UserDTO
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password,
                Role = u.Role
            }).ToList();
        }

        public UserDTO GetUserById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Invalid userId.");
            }

            var filter = Builders<UserDTO>.Filter.Eq(u => u.UserId, userId);
            var userDTO = _dbContext.Users.Find(filter).FirstOrDefault();
            return userDTO;
        }

        public void AddUser(UserDTO user)
        {
            var newUser = new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };

            _dbContext.Users.InsertOne(newUser);
        }

        public void DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Invalid userId.");
            }

            var filter = Builders<UserDTO>.Filter.Eq(u => u.Id, id);
            _dbContext.Users.DeleteOne(filter);
        }
    }
}