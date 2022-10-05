using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApexDataApi.Models
{
    public class Character
    {
        [BsonElement("name")]
        public string CharacterName { get; set; } = null!;

        [BsonElement("playtime")]
        public int Playtime { get; set; }
    }
}
