
namespace LuxuryProperty.Domain.Entities;

public class PropertyImage
{
    public string IdPropertyImage { get; set; } = null!;
    public string IdProperty { get; set; } = null!;
    public string File { get; set; } = null!;
    public bool Enabled { get; set; }
}
