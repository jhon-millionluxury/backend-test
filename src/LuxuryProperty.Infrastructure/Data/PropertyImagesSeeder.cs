using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Infrastructure.Database;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LuxuryProperty.Infrastructure.Data
{
  public class PropertyImagesSeeder(MongoDbContext dbContext, ILogger<PropertyImagesSeeder> logger)
  {
    private readonly MongoDbContext _dbContext = dbContext;
    private readonly ILogger<PropertyImagesSeeder> _logger = logger;

    public async Task SeedAsync()
    {
      var imagesIds = new string[]{
        "https://cdn.millionluxury.com/image-resizing?image=https://azfd-prod.millionluxury.com/spinfile/wwwroot/dist-front/img/MainSite/5333-Collins-Ave-2101.jpg&width=1204&height=840",
        "https://cdn.millionluxury.com/image-resizing?image=https://static.millionluxury.com/spinfile/Spin/Data/Estate/IMG/08778e22949a499e8081cd66bd654a3a.jpg&width=2596&height=1622",
        "https://cdn.millionluxury.com/image-resizing?image=https://azfd-prod.millionluxury.com/spinfile/wwwroot/dist-front/img/MainSite/4-Rivage-24.5M.jpg&width=1204&height=840",
        "https://cdn.millionluxury.com/image-resizing?image=https://azfd-prod.millionluxury.com/spinfile/wwwroot/dist-front/img/MainSite/2-9520-W-Broadview-21.9M.jpg&width=1204&height=840",
        "https://cdn.millionluxury.com/image-resizing?image=https://azfd-prod.millionluxury.com/spinfile/wwwroot/dist-front/img/MainSite/3-Perigon-21.7M.jpg&width=1204&height=840"
      };
      var properties = await _dbContext.Properties.Find(_ => true).ToListAsync();

      if (properties.Count == 0)
      {
        _logger.LogWarning("⚠️ No properties found. Please run PropertySeeder first.");
        return;
      }

      var imageProperties = new List<PropertyImage>();

      foreach (var property in properties)
      {
        var random = new Random();
        int imageCount = random.Next(2, 5);

        for (int i = 1; i <= imageCount; i++)
        {
          imageProperties.Add(new PropertyImage
          {
            IdPropertyImage = Guid.NewGuid().ToString(),
            IdProperty = property.IdProperty,
            File = imagesIds[random.Next(imagesIds.Length)],
            Enabled = true,
          });
        }
      }

      await _dbContext.SeedPropertyImagesAsync(imageProperties);
    }
  }
}
