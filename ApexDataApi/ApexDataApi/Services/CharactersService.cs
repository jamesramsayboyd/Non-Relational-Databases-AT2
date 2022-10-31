using ApexDataApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApexDataApi.Services;

public class CharactersService
{
    private readonly IMongoCollection<Character> _charactersCollection;

    public CharactersService(
        IOptions<ApexPlayerDatabaseSettings> apexPlayerDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            apexPlayerDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            apexPlayerDatabaseSettings.Value.DatabaseName);

        _charactersCollection = mongoDatabase.GetCollection<Character>(
            apexPlayerDatabaseSettings.Value.CharactersCollectionName);
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
