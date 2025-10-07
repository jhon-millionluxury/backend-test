using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace LuxuryProperty.Domain.Entities;

public class Property
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; } = string.Empty;

  public string IdProperty { get; set; } = null!;
  public string Name { get; set; } = null!;
  public string Address { get; set; } = null!;
  public decimal Price { get; set; }
  public string CodeInternal { get; set; } = null!;
  public int Year { get; set; }
  public string Description { get; set; } = null!;
  public string IdOwner { get; set; } = null!;
}
