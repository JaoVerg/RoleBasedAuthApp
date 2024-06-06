using MongoDB.Driver;
using RoleBasedAuthApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoleBasedAuthApp.Data;

namespace RoleBasedAuthApp.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(MongoDbContext context)
        {
            _users = context.Users;
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username) & Builders<User>.Filter.Eq(u => u.Password, password);
            return await _users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            await _users.DeleteOneAsync(filter);
        }
    }
}
