using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RoleBasedAuthApp.Models;

namespace RoleBasedAuthApp.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("newdb");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}
