using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Domain.Repositories;
using LuxuryProperty.Infrastructure.Database;

namespace LuxuryProperty.Infrastructure.Repositories;

public class PropertyImageRepository(MongoDbContext context) : IPropertyImageRepository
{
  private readonly MongoDbContext _context = context;

  public async Task CreateAsync(PropertyImage propertyImage)
  {
    await _context.PropertyImages.InsertOneAsync(propertyImage);
  }

}
