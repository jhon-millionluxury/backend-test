using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Domain.Repositories;
using LuxuryProperty.Infrastructure.Database;
using MongoDB.Driver;

namespace LuxuryProperty.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly MongoDbContext _context;

    public PropertyRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _context.Properties.Find(_ => true).ToListAsync();
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
