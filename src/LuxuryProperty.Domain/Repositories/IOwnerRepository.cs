using LuxuryProperty.Domain.Entities;

namespace LuxuryProperty.Domain.Repositories;

public interface IOwnerRepository
{
  Task CreateAsync(Owner owner);
}
