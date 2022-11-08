using ApexDataApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApexDataApi.Services;

public class CharactersService
{
    private readonly IMongoCollection<Character> _charactersCollection;
    private readonly IMongoCollection<Player> _playersCollection;

    public CharactersService(
        IOptions<ApexPlayerDatabaseSettings> apexPlayerDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            apexPlayerDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            apexPlayerDatabaseSettings.Value.DatabaseName);

        _charactersCollection = mongoDatabase.GetCollection<Character>(
            apexPlayerDatabaseSettings.Value.CharactersCollectionName);
        _playersCollection = mongoDatabase.GetCollection<Player>(
            apexPlayerDatabaseSettings.Value.PlayersCollectionName);
    }

    #region GET
    public async Task<List<Character>> GetAsync() =>
        await _charactersCollection.Find(_ => true).ToListAsync();

    public async Task<Character?> GetAsync(string name) =>
        await _charactersCollection.Find(x => x.CharacterName == name).FirstOrDefaultAsync();

    public async Task<List<Character>> GetPlaytimeAsync()
    {
        List<Character> result = await _charactersCollection.Find(_ => true).ToListAsync();
        result.Sort();
        result.Reverse();
        return result;
    }

    /// <summary>
    /// Loops through a list of all players searching for a specific character
    /// Sums the playtime of that character across all players, updates playtime variable
    /// </summary>
    /// <param name="character"></param>
    public async void CalculatePlaytimeAsync(Character character)
    {
        List<Player> players = await _playersCollection.Find(_ => true).ToListAsync();
        foreach (var player in players)
        {
            if (player.Character1 == character.Id)
            {
                character.Playtime += player.Character1Playtime;
            }
            else if (player.Character2 == character.Id)
            {
                character.Playtime += player.Character2Playtime;
            }
        }
    }

    /// <summary>
    /// Returns a list of all characters, calculating playtime across all players
    /// </summary>
    /// <returns></returns>
    public async Task<List<Character>> GetCharacterList()
    {
        List<Character> result = await _charactersCollection.Find(_ => true).ToListAsync();
        foreach (Character character in result)
        {
            CalculatePlaytimeAsync(character);
        }
        return result;
    }

    /// <summary>
    /// Returns a list of all characters with playtime across all players
    /// List is sorted in order of playtime descending
    /// </summary>
    /// <returns></returns>
    public async Task<List<Character>> GetCharacterListRanked()
    {
        List<Character> result = await _charactersCollection.Find(_ => true).ToListAsync();
        foreach (Character character in result)
        {
            CalculatePlaytimeAsync(character);
        }
        result.Sort();
        result.Reverse();
        return result;
    }
    #endregion GET

    #region POST
    public async Task CreateListAsync(string name1, int playtime1, string name2, int playtime2)
    {
        Character character1 = new Character(name1, playtime1);
        Character character2 = new Character(name2, playtime2);
        List<Character> characterList = new List<Character>() { character1, character2 };
        await _charactersCollection.InsertManyAsync(characterList);
    }
    #endregion POST

    #region DELETE
    public async Task RemoveAsync(string name) =>
        await _charactersCollection.DeleteOneAsync(x => x.CharacterName.ToLower() == name.ToLower());
    #endregion DELETE
}
