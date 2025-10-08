namespace LuxuryProperty.Domain.Entities;

public class PropertyWithImages : Property
{
  public List<PropertyImage> Images { get; set; } = [];
}
