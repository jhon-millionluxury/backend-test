namespace LuxuryProperty.Domain.Entities;

public class PropertyFilters
{
  public string? Name { get; set; }
  public string? Address { get; set; }
  public decimal? MinPrice { get; set; }
  public decimal? MaxPrice { get; set; }
  public int? Page { get; set; }
  public int? PageSize { get; set; }
}