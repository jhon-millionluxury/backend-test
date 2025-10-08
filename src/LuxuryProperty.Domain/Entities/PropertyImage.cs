
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LuxuryProperty.Domain.Entities;

public class PropertyImage
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; } = null!;
  public string IdPropertyImage { get; set; } = null!;
  public string IdProperty { get; set; } = null!;
  public string File { get; set; } = null!;
  public bool Enabled { get; set; }
}
