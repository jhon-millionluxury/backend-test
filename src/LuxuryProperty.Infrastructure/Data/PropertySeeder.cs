using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Infrastructure.Database;

namespace LuxuryProperty.Infrastructure.Data
{
  public class PropertySeeder(MongoDbContext dbContext)
  {
    private readonly MongoDbContext _dbContext = dbContext;

    public async Task SeedAsync()
    {
      var properties = new List<Property>();

      for (int i = 1; i <= 25; i++)
      {
        properties.Add(new Property
        {
          IdProperty = Guid.NewGuid().ToString(),
          Name = $"Property {i}",
          Address = $"Street {i}, City X",
          Price = 100000 + i * 1000,
          CodeInternal = $"CODE{i}",
          Year = 2023,
          Description = $"Description of the property {i}",
          IdOwner = Guid.NewGuid().ToString()
        });
      }

      await _dbContext.SeedPropertiesAsync(properties);
    }
  }
}
