using ApexDataApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApexDataApi.Services;

public class PlayersService
{
    private readonly IMongoCollection<Player> _playersCollection;

    public PlayersService(
        IOptions<ApexPlayerDatabaseSettings> apexPlayerDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            apexPlayerDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            apexPlayerDatabaseSettings.Value.DatabaseName);

        _playersCollection = mongoDatabase.GetCollection<Player>(
            apexPlayerDatabaseSettings.Value.PlayersCollectionName);
    }

    public async Task<List<Player>> GetAsync() =>
        await _playersCollection.Find(_ => true).ToListAsync();

    public async Task<List<Player>> GetRankedAsync()
    {
        List<Player> result = await _playersCollection.Find(_ => true).ToListAsync();
        result.Sort();
        return result;
    }


    public async Task<Player?> GetAsync(string name) =>
        await _playersCollection.Find(x => x.PlayerName == name).FirstOrDefaultAsync();

    public async Task CreateAsync(Player newPlayer) =>
        await _playersCollection.InsertOneAsync(newPlayer);

    public async Task CreateAsync(string name, int rank)
    {
        Player newPlayer = new Player(name, rank);
        await _playersCollection.InsertOneAsync(newPlayer);
    }

    public async Task CreateListAsync(List<Player> playerList)
    {
        await _playersCollection.InsertManyAsync(playerList);
    }

    public async Task UpdateAsync(string id, string name, int rank)
    {
        Player updatedPlayer = new Player(id, name, rank);
        await _playersCollection.ReplaceOneAsync(x => x.Id == id, updatedPlayer);
    }

    public async Task RemoveAsync(string name) =>
        await _playersCollection.DeleteOneAsync(x => x.PlayerName.ToLower() == name.ToLower());
}