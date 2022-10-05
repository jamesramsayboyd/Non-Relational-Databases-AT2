using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApexDataApi.Models;

public class Player
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string PlayerName { get; set; } = null!;

    [BsonElement("rank")]
    public int Rank { get; set; }
}