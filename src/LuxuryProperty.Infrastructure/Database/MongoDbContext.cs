using LuxuryProperty.Domain.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LuxuryProperty.Infrastructure.Database;

public class MongoDbContext
{
  private readonly IMongoDatabase _database;
  private readonly ILogger<MongoDbContext> _logger;

  public MongoDbContext(string connectionString, string databaseName, ILogger<MongoDbContext> logger)
  {
    var client = new MongoClient(connectionString);
    _database = client.GetDatabase(databaseName);

    _logger = logger;
  }

  public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("Owners");
  public IMongoCollection<Property> Properties => _database.GetCollection<Property>("Properties");
  public IMongoCollection<PropertyImage> PropertyImages => _database.GetCollection<PropertyImage>("PropertyImages");
  public IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("PropertyTraces");

  // Methods for seeding the database with sample data
  public async Task SeedPropertiesAsync(IEnumerable<Property> properties)
  {
    var collection = _database.GetCollection<Property>("Properties");

    try
    {
      var count = await collection.CountDocumentsAsync(FilterDefinition<Property>.Empty);
      if (count == 0)
      {
        await collection.InsertManyAsync(properties);
        _logger.LogInformation("✅ Inserted {Count} properties into MongoDB.", properties.Count());
      }
      else
      {
        _logger.LogInformation("ℹ️ Database already contains {Count} properties. Skipping seeding.", count);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "❌ Error occurred while seeding properties.");
    }
  }

  public async Task SeedPropertyImagesAsync(IEnumerable<PropertyImage> propertyImages)
  {
    var collection = _database.GetCollection<PropertyImage>("PropertyImages");

    try
    {
      var count = await collection.CountDocumentsAsync(FilterDefinition<PropertyImage>.Empty);
      if (count == 0)
      {
        await collection.InsertManyAsync(propertyImages);
        _logger.LogInformation("✅ Inserted {Count} property images into MongoDB.", propertyImages.Count());
      }
      else
      {
        _logger.LogInformation("ℹ️ Database already contains {Count} property images. Skipping seeding.", count);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "❌ Error occurred while seeding property images.");
    }
  }

  public async Task SeedOwnersAsync(IEnumerable<Owner> owners)
  {
    var collection = _database.GetCollection<Owner>("Owners");

    try
    {
      var count = await collection.CountDocumentsAsync(FilterDefinition<Owner>.Empty);
      if (count == 0)
      {
        await collection.InsertManyAsync(owners);
        _logger.LogInformation("✅ Inserted {Count} owners into MongoDB.", owners.Count());
      }
      else
      {
        _logger.LogInformation("ℹ️ Database already contains {Count} owners. Skipping seeding.", count);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "❌ Error occurred while seeding owners.");
    }
  }

  public async Task SeedPropertyTracesAsync(IEnumerable<PropertyTrace> propertyTraces)
  {
    var collection = _database.GetCollection<PropertyTrace>("PropertyTraces");

    try
    {
      var count = await collection.CountDocumentsAsync(FilterDefinition<PropertyTrace>.Empty);
      if (count == 0)
      {
        await collection.InsertManyAsync(propertyTraces);
        _logger.LogInformation("✅ Inserted {Count} property traces into MongoDB.", propertyTraces.Count());
      }
      else
      {
        _logger.LogInformation("ℹ️ Database already contains {Count} property traces. Skipping seeding.", count);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "❌ Error occurred while seeding property traces.");
    }
  }
}
