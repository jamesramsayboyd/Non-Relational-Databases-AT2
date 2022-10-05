using ApexDataApi.Models;
using ApexDataApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly PlayersService _playersService;

    public PlayersController(PlayersService playersService) =>
        _playersService = playersService;

    /// <summary>
    /// Shows all Players
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<Player>> Get() =>
        await _playersService.GetAsync();

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
    /// Adds a new Player
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
    /// Adds two new Players in one operation
    /// </summary>
    /// <param name="playerList">A List of Player objects serialized from the request body</param>
    /// <returns></returns>
    [HttpPost("Add multiple")]
    public async Task<IActionResult> PostTwo (List<Player> playerList)
    {
        await _playersService.CreateListAsync(playerList);

        return CreatedAtAction(nameof(Get), new { id = playerList[0].Id }, playerList[0]);
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
    /// <param name="name2">Name of the second player to update</param>
    /// <param name="playerList"></param>
    /// <returns></returns>
    [HttpPut("{name1}/{name2}")]
    public async Task<IActionResult> UpdateMultiple(string name1, string name2, List<Player> playerList)
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