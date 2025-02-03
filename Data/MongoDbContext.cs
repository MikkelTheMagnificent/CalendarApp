using MongoDB.Driver;
using CalendarApp.Models;

namespace CalendarApp.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            // Hent connection string fra appsettings.json
            var connectionString = configuration.GetSection("MongoDb:ConnectionString").Value;
            var client = new MongoClient(connectionString);

            // Hent databasenavnet fra appsettings.json
            var databaseName = configuration.GetSection("MongoDb:Database").Value;
            _database = client.GetDatabase(databaseName);
        }

        // Egenskab for Events-collection
        public IMongoCollection<EventDTO> CalendarEvents =>
            _database.GetCollection<EventDTO>("Events");

        public IMongoCollection<UserDTO> Users =>
            _database.GetCollection<UserDTO>("Users");
    }
}
