using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Domain.Repositories;
using LuxuryProperty.Infrastructure.Database;

namespace LuxuryProperty.Infrastructure.Repositories;

public class PropertyTraceRepository(MongoDbContext context) : IPropertyTraceRepository
{
  private readonly MongoDbContext _context = context;

  public async Task CreateAsync(PropertyTrace propertyTrace)
  {
    await _context.PropertyTraces.InsertOneAsync(propertyTrace);
  }

}
