using LuxuryProperty.Domain.Entities;

namespace LuxuryProperty.Domain.Repositories;

public interface IOwnerRepository
{
    Task<IEnumerable<Owner>> GetAllAsync();
    Task<Owner?> GetByIdAsync(string id);
    Task CreateAsync(Owner owner);
}
