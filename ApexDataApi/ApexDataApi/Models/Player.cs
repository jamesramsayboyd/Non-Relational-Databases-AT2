using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApexDataApi.Models;

public class Player : IComparable
{
    #region CONSTRUCTORS
    /// <summary>
    /// An overloaded constructor allowing all fields to be set
    /// </summary>
    /// <param name="id"></param>
    /// <param name="playerName"></param>
    /// <param name="rank"></param>
    /// <param name="avatar"></param>
    /// <param name="topranked"></param>
    public Player(string? id, string playerName, string avatar, int rank, bool topranked)
    {
        Id = id;
        PlayerName = playerName;
        Avatar = avatar;
        Rank = rank;
        Topranked = topranked;
    }
    public Player(string? id, string playerName, int rank, string avatar)
    {
        Id = id;
        PlayerName = playerName;
        Rank = rank;
        Avatar = avatar;
    }

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
    /// <param name="avatar"></param>
    public Player(string name, int rank, string avatar)
    {
        PlayerName = name;
        Rank = rank;
        Avatar = avatar;
    }
    #endregion CONSTRUCTORS

    /// <summary>
    /// The CompareTo function for the IComparable interface
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int CompareTo(object? obj)
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

    [BsonElement("avatar")]
    public string Avatar { get; set; }

    [BsonElement("topranked")]
    public bool Topranked { get; set; }
}