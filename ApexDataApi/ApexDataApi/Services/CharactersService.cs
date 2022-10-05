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

    public async Task<List<Character>> GetAsync() =>
        await _charactersCollection.Find(_ => true).ToListAsync();

    public async Task CreateListAsync(List<Character> characterList)
    {
        await _charactersCollection.InsertManyAsync(characterList);
    }
}
