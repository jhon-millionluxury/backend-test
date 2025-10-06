using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Domain.Repositories;
using LuxuryProperty.Infrastructure.Database;
using MongoDB.Driver;

namespace LuxuryProperty.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly MongoDbContext _context;

    public OwnerRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Owner>> GetAllAsync()
    {
        return await _context.Owners.Find(_ => true).ToListAsync();
    }

    public async Task<Owner?> GetByIdAsync(string id)
    {
        return await _context.Owners.Find(o => o.IdOwner == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Owner owner)
    {
        await _context.Owners.InsertOneAsync(owner);
    }
}
