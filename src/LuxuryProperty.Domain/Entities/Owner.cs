
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LuxuryProperty.Domain.Entities;

public class Owner
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; } = null!;
  public string IdOwner { get; set; } = null!;
  public string Name { get; set; } = null!;
  public string Address { get; set; } = null!;
  public string Photo { get; set; } = null!;
  public DateTime Birthday { get; set; }
}
