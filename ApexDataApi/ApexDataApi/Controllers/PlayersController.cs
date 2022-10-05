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

    //public PlayersController(PlayersService playersService) =>
    //    _playersService = playersService;

    public PlayersController(PlayersService playersService, CharactersService charactersService)
    {
        _playersService = playersService;
        _charactersService = charactersService;
    }

    /// <summary>
    /// Selects all Players
    /// </summary>
    /// <returns></returns>
    [HttpGet("Players")]
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

    /// <summary>
    /// Inserts a Player
    /// </summary>
    /// <param name="newPlayer">The new Player object to be added</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(Player newPlayer)
    {
        await _playersService.CreateAsync(newPlayer);

        return CreatedAtAction(nameof(Get), new { id = newPlayer.Id }, newPlayer);
    }


    /// <summary>
    /// Inserts two Characters in one operation
    /// </summary>
    /// <param name="characterList">A List of Character objects serialized from the request body</param>
    /// <returns></returns>
    [HttpPost("MultipleCharacters")]
    public async Task<IActionResult> PostTwo (List<Character> characterList)
    {
        await _charactersService.CreateListAsync(characterList);

        return CreatedAtAction(nameof(Get), new { id = characterList[0].Id }, characterList[0]);
    }

    /// <summary>
    /// Updates a Player
    /// </summary>
    /// <param name="name">The name of the player to be updated</param>
    /// <param name="updatedPlayer"></param>
    /// <returns></returns>
    [HttpPut("{name}")]
    public async Task<IActionResult> Update(string name, Player updatedPlayer)
    {
        var player = await _playersService.GetAsync(name);

        if (player is null)
            return NotFound();

        updatedPlayer.Id = player.Id;

        await _playersService.UpdateAsync(name, updatedPlayer);

        return NoContent();
    }

    /// <summary>
    /// Updates multiple Players in one operation
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

    /// <summary>
    /// Deletes a specific player
    /// </summary>
    /// <param name="name">Name of the player to delete (case-insensitive)</param>
    /// <returns></returns>
    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        var player = await _playersService.GetAsync(name);

        if (player is null)
        {
            return NotFound();
        }

        await _playersService.RemoveAsync(name);

        return NoContent();
    }

    /// <summary>
    /// Deletes multiple Players in one operation
    /// </summary>
    /// <param name="name1">Name of the first player to delete</param>
    /// <param name="name2">Name of the second player to delete</param>
    /// <returns></returns>
    [HttpDelete("{name1}/{name2}")]
    public async Task<IActionResult> Delete(string name1, string name2)
    {
        var player = await _playersService.GetAsync(name1);

        if (player is null)
            return NotFound();

        var player2 = await _playersService.GetAsync(name2);

        if (player2 is null)
            return NotFound();

        await _playersService.RemoveAsync(name1);
        await _playersService.RemoveAsync(name2);

        return NoContent();
    }
}