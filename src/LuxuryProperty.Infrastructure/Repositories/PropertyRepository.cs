using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Domain.Repositories;
using LuxuryProperty.Infrastructure.Database;
using MongoDB.Driver;

namespace LuxuryProperty.Infrastructure.Repositories;

public class PropertyRepository(MongoDbContext context) : IPropertyRepository
{
  private readonly IMongoCollection<Property> _properties = context.Properties;
  private readonly MongoDbContext _context = context;

  public async Task<IEnumerable<Property>> GetByFiltersAsync(PropertyFilters filters)
  {
    var filterBuilder = Builders<Property>.Filter;

    var localFilters = new List<FilterDefinition<Property>>();

    if (!string.IsNullOrEmpty(filters.Name))
      localFilters.Add(filterBuilder.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(filters.Name, "i")));

    if (!string.IsNullOrEmpty(filters.Address))
      localFilters.Add(filterBuilder.Regex(x => x.Address, new MongoDB.Bson.BsonRegularExpression(filters.Address, "i")));

    if (filters.MinPrice.HasValue)
      localFilters.Add(filterBuilder.Gte(x => x.Price, filters.MinPrice.Value));

    if (filters.MaxPrice.HasValue)
      localFilters.Add(filterBuilder.Lte(x => x.Price, filters.MaxPrice.Value));

    var finalFilter = localFilters.Count > 0
                ? filterBuilder.And(localFilters)
                : filterBuilder.Empty;

    int page = filters.Page ?? 1;
    int pageSize = filters.PageSize ?? 10;
    int skip = (page - 1) * pageSize;

    return await _properties
        .Find(finalFilter)
        .Skip(skip)
        .Limit(pageSize)
        .ToListAsync();

  }

  public async Task<Property?> GetByIdAsync(string id)
  {
    return await _context.Properties.Find(p => p.IdProperty == id).FirstOrDefaultAsync();
  }

  public async Task CreateAsync(Property property)
  {
    await _context.Properties.InsertOneAsync(property);
  }

  public async Task UpdateAsync(string id, Property property)
  {
    await _context.Properties.ReplaceOneAsync(p => p.IdProperty == id, property);
  }

  public async Task DeleteAsync(string id)
  {
    await _context.Properties.DeleteOneAsync(p => p.IdProperty == id);
  }
}
