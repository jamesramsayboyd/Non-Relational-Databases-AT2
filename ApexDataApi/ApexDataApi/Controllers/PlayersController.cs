using ApexDataApi.Models;
using ApexDataApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApexDataApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly PlayersService _playersService;
    private readonly CharactersService _charactersService;

    public PlayersController(PlayersService playersService, CharactersService charactersService)
    {
        _playersService = playersService;
        _charactersService = charactersService;
    }

    #region SELECT PLAYERS/CHARACTERS
    /// <summary>
    /// Selects all Players
    /// </summary>
    /// <returns></returns>
    [HttpGet("All")]
    public async Task<List<Player>> Get() =>
        await _playersService.GetAsync();


    /// <summary>
    /// Selects all Characters
    /// </summary>
    /// <returns></returns>
    [HttpGet("Characters")]
    public async Task<List<Character>> GetCharacters() =>
        await _charactersService.GetAsync();

    /// <summary>
    /// Selects a Player by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("{name}")]
    public async Task<ActionResult<Player>> Get(string name)
    {
        var player = await _playersService.GetAsync(name);

        if (player is null)
        {
            return NotFound();
        }

        return player;
    }
    #endregion SORT PLAYERS/CHARACTERS

    #region SORT PLAYERS/CHARACTERS
    /// <summary>
    /// Sorts all Players by Rank
    /// </summary>
    /// <returns></returns>
    [HttpGet("PlayersByRank")]
    public async Task<List<Player>> GetRanked() =>
        await _playersService.GetRankedAsync();

    /// <summary>
    /// Sorts all Characters by Playtime
    /// </summary>
    /// <returns></returns>
    [HttpGet("CharactersByPlaytime")]
    public async Task<List<Character>> GetCharactersByPlaytime() =>
        await _charactersService.GetPlaytimeAsync();
    #endregion SORT PLAYERS/CHARACTERS

    #region INSERT PLAYERS
    /// <summary>
    /// Inserts a Player
    /// </summary>
    /// <param name="name">The player's name</param>
    /// <param name="rank">The player's rank</param>
    /// <returns></returns>
    [HttpPost("{name}/{rank}")]
    public async Task<IActionResult> Post(string name, int rank)
    {
        await _playersService.CreateAsync(name, rank);

        //return CreatedAtAction(nameof(Get), new { id = 0 }, 0);
        return CreatedAtAction(nameof(Get), 0, 0);
    }


    /// <summary>
    /// Inserts two Characters in one operation
    /// </summary>
    /// <param name="characterList">A List of Character objects serialized from the request body</param>
    /// <returns></returns>
    [HttpPost("{name1}/{playtime1}/{name2}/{playtime2}")]
    public async Task<IActionResult> PostTwo (string name1, int playtime1, string name2, int playtime2)
    {
        await _charactersService.CreateListAsync(name1, playtime1, name2, playtime2);

        return CreatedAtAction(nameof(Get), 0, 0);
    }
    #endregion INSERT PLAYERS

    #region UPDATE PLAYER RANKING
    /// <summary>
    /// Updates a Player's ranking
    /// </summary>
    /// <param name="name">The name of the player to be updated</param>
    /// <param name="rank">The new rank</param>
    /// <returns></returns>
    [HttpPut("{name}/{rank}")]
    public async Task<IActionResult> Update(string name, int rank)
    {
        var player = await _playersService.GetAsync(name);

        if (player is null)
            return NotFound();

        await _playersService.UpdateAsync(player.Id, name, rank);

        return CreatedAtAction(nameof(Get), 0, 0);
    }    

    /// <summary>
    /// Updates multiple Players' Rankings in one operation
    /// </summary>
    /// <param name="name1">Name of the first player to update</param>
    /// <param name="rank1">The new ranking</param>
    /// <param name="name2">Name of the second player to update</param>
    /// <param name="rank2">The new ranking</param>
    /// <param name="playerList"></param>
    /// <returns></returns>
    [HttpPut("{name1}/{rank1}/{name2}/{rank2}")]
    public async Task<IActionResult> UpdateMultiple(string name1, string rank1, string name2, string rank2, List<Player> playerList)
    {
        var player1 = await _playersService.GetAsync(name1);

        if (player1 is null)
            return NotFound();

        var player2 = await _playersService.GetAsync(name2);

        if (player2 is null)
            return NotFound();

        await _playersService.RemoveAsync(name1);
        await _playersService.RemoveAsync(name2);

        await _playersService.CreateListAsync(playerList);

        return CreatedAtAction(nameof(Get), new { id = playerList[0].Id }, playerList[0]);
    }
    #endregion UPDATE PLAYER RANKING

    #region DELETE CHARACTERS
    /// <summary>
    /// Deletes a specific Character
    /// </summary>
    /// <param name="name">Name of the character to delete (case-insensitive)</param>
    /// <returns></returns>
    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        var character = await _charactersService.GetAsync(name);

        if (character is null)
        {
            return NotFound();
        }

        await _charactersService.RemoveAsync(name);

        return NoContent();
    }

    /// <summary>
    /// Deletes multiple Characters in one operation
    /// </summary>
    /// <param name="name1">Name of the first character to delete</param>
    /// <param name="name2">Name of the second character to delete</param>
    /// <returns></returns>
    [HttpDelete("{name1}/{name2}")]
    public async Task<IActionResult> Delete(string name1, string name2)
    {
        var character = await _charactersService.GetAsync(name1);

        if (character is null)
            return NotFound();

        var character2 = await _charactersService.GetAsync(name2);

        if (character2 is null)
            return NotFound();

        await _charactersService.RemoveAsync(name1);
        await _charactersService.RemoveAsync(name2);

        return NoContent();
    }
    #endregion DELETE CHARACTERS
}