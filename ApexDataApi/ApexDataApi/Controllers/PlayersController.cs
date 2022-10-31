using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApexDataApi.Data;
using ApexDataApi.Models;
using ApexDataApi.Services;

namespace ApexDataApi.Controllers
{
    #region default stuff
    //public class PlayersController : Controller
    //{
    //    private readonly ApexDataApiContext _context;

    //    public PlayersController(ApexDataApiContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: Players
    //    public async Task<IActionResult> Index()
    //    {
    //          return View(await _context.Player.ToListAsync());
    //    }

    //    // GET: Players/Details/5
    //    public async Task<IActionResult> Details(string id)
    //    {
    //        if (id == null || _context.Player == null)
    //        {
    //            return NotFound();
    //        }

    //        var player = await _context.Player
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (player == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(player);
    //    }

    //    // GET: Players/Create
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: Players/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("Id,PlayerName,Rank,Avatar,Topranked")] Player player)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _context.Add(player);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(player);
    //    }

    //    // GET: Players/Edit/5
    //    public async Task<IActionResult> Edit(string id)
    //    {
    //        if (id == null || _context.Player == null)
    //        {
    //            return NotFound();
    //        }

    //        var player = await _context.Player.FindAsync(id);
    //        if (player == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(player);
    //    }

    //    // POST: Players/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(string id, [Bind("Id,PlayerName,Rank,Avatar,Topranked")] Player player)
    //    {
    //        if (id != player.Id)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(player);
    //                await _context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!PlayerExists(player.Id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(player);
    //    }

    //    // GET: Players/Delete/5
    //    public async Task<IActionResult> Delete(string id)
    //    {
    //        if (id == null || _context.Player == null)
    //        {
    //            return NotFound();
    //        }

    //        var player = await _context.Player
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (player == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(player);
    //    }

    //    // POST: Players/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(string id)
    //    {
    //        if (_context.Player == null)
    //        {
    //            return Problem("Entity set 'ApexDataApiContext.Player'  is null.");
    //        }
    //        var player = await _context.Player.FindAsync(id);
    //        if (player != null)
    //        {
    //            _context.Player.Remove(player);
    //        }

    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool PlayerExists(string id)
    //    {
    //      return _context.Player.Any(e => e.Id == id);
    //    }
    //}
    #endregion default stuffnamespace ApexDataApi.Controllers;

    [ApiController]
    //[Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly ApexDataApiContext _context;
        private readonly PlayersService _playersService;

        public PlayersController(PlayersService playersService)
        {
            _playersService = playersService;
        }

        #region SELECT PLAYERS/CHARACTERS
        /// <summary>
        /// Selects all Players
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllPlayers")]
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
        #endregion SORT PLAYERS/CHARACTERS

        #region SORT PLAYERS/CHARACTERS
        /// <summary>
        /// Sorts all Players by Rank
        /// </summary>
        /// <returns></returns>
        [HttpGet("PlayersByRank")]
        public async Task<List<Player>> GetRanked() =>
            await _playersService.GetRankedListAsync();

         #endregion SORT PLAYERS/CHARACTERS

        #region INSERT PLAYERS
        /// <summary>
        /// Inserts a Player
        /// </summary>
        /// <param name="name">The player's name</param>
        /// <param name="rank">The player's rank</param>
        /// <param name="avatar">A link to the player's avatar image</param>
        /// <returns></returns>
        [HttpPost("{name}/{rank}/{avatar}")]
        public async Task<IActionResult> Post(string name, int rank, string avatar)
        {
            await _playersService.CreateAsync(name, rank, avatar);

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

            await _playersService.UpdateRankAsync(player, rank);

            return CreatedAtAction(nameof(Get), 0, 0);
        }

        /// <summary>
        /// Updates multiple Players' Rankings in one operation
        /// </summary>
        /// <param name="name1">Name of the first player to update</param>
        /// <param name="rank1">The new ranking</param>
        /// <param name="name2">Name of the second player to update</param>
        /// <param name="rank2">The new ranking</param>
        /// <returns></returns>
        [HttpPut("{name1}/{rank1}/{name2}/{rank2}")]
        public async Task<IActionResult> UpdateMultiple(string name1, int rank1, string name2, int rank2)
        {
            var player1 = await _playersService.GetAsync(name1);

            if (player1 is null)
                return NotFound();

            var player2 = await _playersService.GetAsync(name2);

            if (player2 is null)
                return NotFound();

            await _playersService.UpdateMultipleRanksAsync(player1, rank1, player2, rank2);

            return CreatedAtAction(nameof(Get), 0, 0);
        }
        #endregion UPDATE PLAYER RANKING
    }
}
