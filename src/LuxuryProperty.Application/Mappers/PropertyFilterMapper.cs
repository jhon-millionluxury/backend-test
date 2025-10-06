using LuxuryProperty.Application.DTOs;
using LuxuryProperty.Domain.Entities;

namespace LuxuryProperty.Application.Mappers
{
  public static class PropertyFilterMapper
  {
    public static PropertyFilters ToDomain(this PropertyFilterDto dto)
    {
      return new PropertyFilters
      {
        Name = dto.Name,
        Address = dto.Address,
        MinPrice = dto.MinPrice,
        MaxPrice = dto.MaxPrice,
        Page = dto.Page,
        PageSize = dto.PageSize
      };
    }
  }
}
