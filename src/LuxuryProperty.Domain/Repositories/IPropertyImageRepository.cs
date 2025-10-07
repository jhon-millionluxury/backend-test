using LuxuryProperty.Domain.Entities;

namespace LuxuryProperty.Domain.Repositories;

public interface IPropertyImageRepository
{
  Task CreateAsync(PropertyImage propertyImage);
}
