using LuxuryProperty.Domain.Entities;

namespace LuxuryProperty.Domain.Repositories;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(string id);
    Task CreateAsync(Property property);
    Task UpdateAsync(string id, Property property);
    Task DeleteAsync(string id);
}
