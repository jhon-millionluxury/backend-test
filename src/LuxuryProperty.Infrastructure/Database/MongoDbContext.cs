using LuxuryProperty.Domain.Entities;
using MongoDB.Driver;

namespace LuxuryProperty.Infrastructure.Database;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("Owners");
    public IMongoCollection<Property> Properties => _database.GetCollection<Property>("Properties");
    public IMongoCollection<PropertyImage> PropertyImages => _database.GetCollection<PropertyImage>("PropertyImages");
    public IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("PropertyTraces");
}
