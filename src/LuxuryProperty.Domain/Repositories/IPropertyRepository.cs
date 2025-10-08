using LuxuryProperty.Domain.Common;
using LuxuryProperty.Domain.Entities;

namespace LuxuryProperty.Domain.Repositories;

public interface IPropertyRepository
{
  Task<PropertyWithImages?> GetByIdAsync(string id);
  Task CreateAsync(Property property);
  Task UpdateAsync(string id, Property property);
  Task DeleteAsync(string id);
  Task<PagedResult<PropertyWithImages>> GetByFiltersAsync(PropertyFilters filters);
}
