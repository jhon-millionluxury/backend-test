using LuxuryProperty.Application.Services;
using LuxuryProperty.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using LuxuryProperty.Application.DTOs;
using LuxuryProperty.Application.Mappers;

namespace LuxuryProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController(PropertyService propertyService) : ControllerBase
{
  private readonly PropertyService _propertyService = propertyService;

  [HttpGet]
  public async Task<IActionResult> GetByFilters([FromQuery] PropertyFilterDto filtersDto)
  {
    var filters = filtersDto.ToDomain();

    var properties = await _propertyService.GetPropertiesByFiltersAsync(filters);
    return Ok(properties);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(string id)
  {
    var property = await _propertyService.GetPropertyByIdAsync(id);
    return property is null ? NotFound() : Ok(property);
  }

  [HttpPost]
  public async Task<IActionResult> Create(Property property)
  {
    await _propertyService.CreatePropertyAsync(property);
    return CreatedAtAction(nameof(GetById), new { id = property.IdProperty }, property);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(string id, Property property)
  {
    await _propertyService.UpdatePropertyAsync(id, property);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(string id)
  {
    await _propertyService.DeletePropertyAsync(id);
    return NoContent();
  }
}
