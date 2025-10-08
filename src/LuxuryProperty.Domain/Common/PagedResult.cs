namespace LuxuryProperty.Domain.Common
{
  public class PagedResult<T>
  {
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public long TotalResults { get; set; }
    public IEnumerable<T> Items { get; set; } = [];
  }
}
