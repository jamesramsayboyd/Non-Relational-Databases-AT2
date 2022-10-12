using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApexDataApi.Models;

public class Player : IComparable
{
    /// <summary>
    /// An overloaded constructor allowing Id, PlayerName and Rank fields to be set
    /// </summary>
    /// <param name="id"></param>
    /// <param name="playerName"></param>
    /// <param name="rank"></param>
    public Player(string? id, string playerName, int rank)
    {
        Id = id;
        PlayerName = playerName;
        Rank = rank;
    }

    /// <summary>
    /// An overloaded constructor allowing PlayerName and Rank fields to be set
    /// </summary>
    /// <param name="name"></param>
    /// <param name="rank"></param>
    public Player(string name, int rank)
    {
        PlayerName = name;
        Rank = rank;
    }

    public int CompareTo(object obj)
    {
        Player? compare = obj as Player;
        return Rank.CompareTo(compare?.Rank);
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string PlayerName { get; set; } = null!;

    [BsonElement("rank")]
    public int Rank { get; set; }
}