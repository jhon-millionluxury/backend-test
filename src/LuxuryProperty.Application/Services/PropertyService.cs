using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Domain.Repositories;

namespace LuxuryProperty.Application.Services;

public class PropertyService
{
    private readonly IPropertyRepository _repository;

    public PropertyService(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        => await _repository.GetAllAsync();

    public async Task<Property?> GetPropertyByIdAsync(string id)
        => await _repository.GetByIdAsync(id);

    public async Task CreatePropertyAsync(Property property)
        => await _repository.CreateAsync(property);

    public async Task UpdatePropertyAsync(string id, Property property)
        => await _repository.UpdateAsync(id, property);

    public async Task DeletePropertyAsync(string id)
        => await _repository.DeleteAsync(id);
}
