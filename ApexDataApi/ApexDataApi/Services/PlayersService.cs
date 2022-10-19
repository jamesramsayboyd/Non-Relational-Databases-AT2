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

    #region GET
    public async Task<List<Player>> GetAsync() =>
        await _playersCollection.Find(_ => true).ToListAsync();

    public async Task<List<Player>> GetRankedListAsync()
    {
        List<Player> result = await _playersCollection.Find(_ => true).ToListAsync();
        result.Sort();
        return result;
    }

    public async Task<Player?> GetAsync(string name) =>
        await _playersCollection.Find(x => x.PlayerName == name).FirstOrDefaultAsync();

    public async Task<Player?> GetRank(int rank) =>
        await _playersCollection.Find(x => x.Rank == rank).FirstOrDefaultAsync();
    #endregion GET

    #region POST
    public async Task CreateAsync(Player newPlayer) =>
        await _playersCollection.InsertOneAsync(newPlayer);

    public async Task CreateAsync(string name, int rank, string avatar)
    {
        Player newPlayer = new Player(name, rank, avatar);
        await _playersCollection.InsertOneAsync(newPlayer);
    }

    public async Task CreateListAsync(List<Player> playerList)
    {
        await _playersCollection.InsertManyAsync(playerList);
    }
    #endregion POST

    #region PUT
    public async Task UpdateAsync(string id, string name, int rank)
    {
        Player updatedPlayer = new Player(id, name, rank);
        List<Player> players = await GetAsync();
        players.Sort();

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].Rank >= rank)
            {
                players[i].Rank += 1;
                await _playersCollection.ReplaceOneAsync(x => x.Id == players[i].Id, players[i]);
            }
        }
        await _playersCollection.ReplaceOneAsync(x => x.Id == id, updatedPlayer);
    }

    public async Task UpdateRankAsync(Player currentPlayer, int newrank)
    {
        Player updatedPlayer = new Player(currentPlayer.Id, currentPlayer.PlayerName, currentPlayer.Avatar, newrank, false);
        if (updatedPlayer.Rank == 1)
        {
            updatedPlayer.Topranked = true;
        }
        List<Player> players = await GetAsync();
        players.Sort();

        if (newrank < currentPlayer.Rank)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Rank >= newrank)
                {
                    if (players[i].Rank < currentPlayer.Rank)
                    //if (players[i].Rank < players.Count)
                    {
                        players[i].Rank += 1;
                        if (players[i].Rank == 1)
                        {
                            players[i].Topranked = true;
                        }
                        else
                        {
                            players[i].Topranked = false;
                        }
                        await _playersCollection.ReplaceOneAsync(x => x.Id == players[i].Id, players[i]);
                    }                    
                }
            }
            await _playersCollection.ReplaceOneAsync(x => x.Id == currentPlayer.Id, updatedPlayer);
        }
        else if (newrank > currentPlayer.Rank)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Rank <= newrank)
                {
                    if (players[i].Rank > 1)
                    {
                        players[i].Rank -= 1;
                        if (players[i].Rank == 1)
                        {
                            players[i].Topranked = true;
                        }
                        else
                        {
                            players[i].Topranked = false;
                        }
                        await _playersCollection.ReplaceOneAsync(x => x.Id == players[i].Id, players[i]);
                    }                    
                }                    
            }
            await _playersCollection.ReplaceOneAsync(x => x.Id == currentPlayer.Id, updatedPlayer);
        }
    }

    public async Task UpdateMultipleRanksAsync(Player player1, int newrank1, Player player2, int newrank2)
    {
        await UpdateRankAsync(player1, newrank1);
        await UpdateRankAsync(player2, newrank2);
    }
    #endregion PUT

    #region DELETE
    public async Task RemoveAsync(string name) =>
        await _playersCollection.DeleteOneAsync(x => x.PlayerName.ToLower() == name.ToLower());
    #endregion DELETE
}