using LuxuryProperty.Domain.Common;
using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Domain.Repositories;
using LuxuryProperty.Infrastructure.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LuxuryProperty.Infrastructure.Repositories;

public class PropertyRepository(MongoDbContext context) : IPropertyRepository
{
  private readonly MongoDbContext _context = context;
  private readonly IMongoCollection<Property> _properties = context.Properties;
  private readonly IMongoCollection<PropertyImage> _propertyImages = context.PropertyImages;

  public async Task<PagedResult<PropertyWithImages>> GetByFiltersAsync(PropertyFilters filters)
  {
    var pipeline = new List<BsonDocument>();

    // dynamic filters
    var filter = new BsonDocument();
    if (!string.IsNullOrEmpty(filters.Name))
      filter.Add("Name", new BsonDocument("$regex", filters.Name).Add("$options", "i"));
    if (!string.IsNullOrEmpty(filters.Address))
      filter.Add("Address", new BsonDocument("$regex", filters.Address).Add("$options", "i"));

    if (filters.MinPrice.HasValue || filters.MaxPrice.HasValue)
    {
      var priceFilter = new BsonDocument();

      if (filters.MinPrice.HasValue)
        priceFilter.Add("$gte", filters.MinPrice.Value);

      if (filters.MaxPrice.HasValue)
        priceFilter.Add("$lte", filters.MaxPrice.Value);

      filter.Add("Price", priceFilter);
    }

    if (filter.ElementCount > 0)
      pipeline.Add(new BsonDocument("$match", filter));

    // Lookup to join with Images collection
    pipeline.Add(new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "PropertyImages" },
                { "localField", "IdProperty" },
                { "foreignField", "IdProperty" },
                { "as", "Images" }
            }));

    // Total count
    var totalCountPipeline = new List<BsonDocument>(pipeline)
            {
                new("$count", "count")
            };

    var totalCountResult = await _properties.Aggregate<BsonDocument>(totalCountPipeline).FirstOrDefaultAsync();
    var totalResults = totalCountResult?["count"].AsInt32 ?? 0;

    // Pagination
    var page = filters.Page ?? 1;
    var pageSize = filters.PageSize ?? 10;
    var totalPages = (int)Math.Ceiling((double)totalResults / pageSize);

    pipeline.Add(new("$skip", (page - 1) * pageSize));
    pipeline.Add(new("$limit", pageSize));

    var results = await _properties.Aggregate<PropertyWithImages>(pipeline).ToListAsync();

    return new PagedResult<PropertyWithImages>
    {
      CurrentPage = page,
      TotalPages = totalPages,
      TotalResults = totalResults,
      Items = results
    };
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
