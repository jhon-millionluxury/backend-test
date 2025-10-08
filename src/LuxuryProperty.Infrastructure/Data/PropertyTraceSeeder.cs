using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Infrastructure.Database;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LuxuryProperty.Infrastructure.Data
{
  public class PropertyTraceSeeder(MongoDbContext dbContext, ILogger<PropertyTraceSeeder> logger)
  {
    private readonly MongoDbContext _dbContext = dbContext;
    private readonly ILogger<PropertyTraceSeeder> _logger = logger;

    public async Task SeedAsync()
    {
      var existingCount = await _dbContext.PropertyTraces.CountDocumentsAsync(FilterDefinition<PropertyTrace>.Empty);
      if (existingCount > 0)
      {
        _logger.LogWarning("⚠️ Exists property traces. Skipping seeding.");
        return;
      }

      var properties = await _dbContext.Properties.Find(_ => true).ToListAsync();

      if (properties.Count == 0)
      {
        _logger.LogWarning("⚠️ No properties found. Please run PropertySeeder first.");
        return;
      }

      var random = new Random();
      var propertyTraces = new List<PropertyTrace>();

      for (int i = 1; i <= properties.Count; i++)
      {
        var property = properties[random.Next(properties.Count)];

        propertyTraces.Add(new PropertyTrace
        {
          IdPropertyTrace = Guid.NewGuid().ToString(),
          DateSale = DateTime.Now.AddDays(-i),
          Name = $"Property {i}",
          Value = 100000 + i * 1000,
          Tax = 0.1m,
          IdProperty = property.IdProperty
        });
      }

      await _dbContext.SeedPropertyTracesAsync(propertyTraces);

      _logger.LogInformation($"✅ Inserted {propertyTraces.Count} property traces.");
    }
  }
}
