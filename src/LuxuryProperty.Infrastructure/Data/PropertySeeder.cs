using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Infrastructure.Database;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LuxuryProperty.Infrastructure.Data
{
  public class PropertySeeder(MongoDbContext dbContext, ILogger<PropertySeeder> logger)
  {
    private readonly MongoDbContext _dbContext = dbContext;
    private readonly ILogger<PropertySeeder> _logger = logger;

    public async Task SeedAsync()
    {
      string[] features = ["Bedrooms", "Bathrooms", "Kitchen", "Living Room", "Garage"];

      var existingCount = await _dbContext.Properties.CountDocumentsAsync(FilterDefinition<Property>.Empty);
      if (existingCount > 0)
      {
        _logger.LogWarning("ℹ️ Database already contains {Count} property traces. Skipping seeding.", existingCount);
        return;
      }

      var owners = await _dbContext.Owners.Find(_ => true).ToListAsync();
      if (owners.Count == 0)
      {
        _logger.LogWarning("⚠️ Owners were not found. Please run OwnerSeeder first.");
        return;
      }

      var random = new Random();
      var properties = new List<Property>();

      for (int i = 1; i <= 25; i++)
      {
        var owner = owners[random.Next(owners.Count)];

        properties.Add(new Property
        {
          IdProperty = Guid.NewGuid().ToString(),
          Name = $"Property {i}",
          Address = $"Street {i}, City X",
          Price = 100000 + i * 100000,
          CodeInternal = $"CODE{i}",
          Year = 2023,
          Description = $"Description of the property {i}",
          IdOwner = owner.IdOwner,
          Features = [.. features.OrderBy(_ => random.Next()).Take(random.Next(1, features.Length + 1))],
          Featured = i % 2 == 0,
          Bedrooms = random.Next(1, 5),
          Bathrooms = random.Next(1, 5),
          Sqft = random.Next(500, 10000)
        });
      }

      await _dbContext.SeedPropertiesAsync(properties);
    }
  }
}
