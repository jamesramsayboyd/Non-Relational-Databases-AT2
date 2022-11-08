using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using ApexDataApi.Data;
using ApexDataApi.Models;
using ApexDataApi.Services;
using Microsoft.AspNetCore.Authorization;

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

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly PlayersService _playersService;

        public PlayersController(PlayersService playersService)
        {
            _playersService = playersService;
        }

        // Show list of all players
        [HttpGet("Admin"), Route("index"), ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _playersService.GetAsync());
            //return View(await _playersService.GetRankedListAsync());
        }

        [HttpGet("Index"), Route("index"), ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Index()
        {
            //return View(await _playersService.GetAsync());
            return View(await _playersService.GetRankedListAsync());
        }

        // Show a single player's details
        [HttpGet("PlayerDetails"), Route("details"), ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Details(string id)
        {
            var player = await _playersService.GetAsyncId(id);

            if (player is null)
            {
                return NotFound();
            }

            return View(player);
        }
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.Player == null)
        //    {
        //        return NotFound();
        //    }

        //    var player = await _context.Player
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (player == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(player);
        //}

        // Create a new player
        [HttpGet("Player"), Route("create"), ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerName,Rank,Avatar,Topranked")] Player player)
        {
            if (ModelState.IsValid)
            {
                await _playersService.CreateAsync(player);
            }
            return View(player);
        }
        // GET: Players/Create
        //[HttpGet("Player"), Route("create"), ApiExplorerSettings(IgnoreApi = true)]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,PlayerName,Rank,Avatar,Topranked")] Player player)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _playersService.CreateAsync(player);
        //        //_context.Add(player);
        //        //await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(player);
        //}

        //// GET: Players/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null || _context.Player == null)
        //    {
        //        return NotFound();
        //    }

        //    var player = await _context.Player.FindAsync(id);
        //    if (player == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(player);
        //}

        //// POST: Players/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,PlayerName,Rank,Avatar,Topranked")] Player player)
        //{
        //    if (id != player.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(player);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PlayerExists(player.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(player);
        //}

        //// GET: Players/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null || _context.Player == null)
        //    {
        //        return NotFound();
        //    }

        //    var player = await _context.Player
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (player == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(player);
        //}

        //// POST: Players/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    if (_context.Player == null)
        //    {
        //        return Problem("Entity set 'ApexDataApiContext.Player'  is null.");
        //    }
        //    var player = await _context.Player.FindAsync(id);
        //    if (player != null)
        //    {
        //        _context.Player.Remove(player);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PlayerExists(string id)
        //{
        //    return _context.Player.Any(e => e.Id == id);
        //}
    }
}
