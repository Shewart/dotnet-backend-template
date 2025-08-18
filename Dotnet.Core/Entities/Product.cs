using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dotnet.Core.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("category")]
    public required string Category { get; set; }

    [BsonElement("inStock")]
    public bool InStock { get; set; } = true;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
