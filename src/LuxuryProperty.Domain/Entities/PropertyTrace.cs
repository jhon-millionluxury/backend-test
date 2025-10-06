namespace LuxuryProperty.Domain.Entities;

public class PropertyTrace
{
    public string IdPropertyTrace { get; set; } = null!;
    public DateTime DateSale { get; set; }
    public string Name { get; set; } = null!;
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
    public string IdProperty { get; set; } = null!;
}
