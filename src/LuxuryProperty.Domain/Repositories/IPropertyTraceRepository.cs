using LuxuryProperty.Domain.Entities;

namespace LuxuryProperty.Domain.Repositories;

public interface IPropertyTraceRepository
{
  Task CreateAsync(PropertyTrace propertyTrace);
}
