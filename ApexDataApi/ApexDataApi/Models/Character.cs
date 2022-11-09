using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApexDataApi.Models;

public class Character : IComparable
{
    #region CONSTRUCTORS
    public Character() { }

    public Character(string characterName, int playtime)
    {
        CharacterName = characterName;
        Playtime = playtime;
    }
    #endregion CONSTRUCTORS
    public int CompareTo(object obj)
    {
        Character? compare = obj as Character;
        return Playtime.CompareTo(compare?.Playtime);
    }

    //[BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("Id")]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string CharacterName { get; set; } = null!;

    [BsonElement("playtime")]
    public int Playtime { get; set; }
}
