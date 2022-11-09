using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApexDataApi.Models;
using ApexDataApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApexDataApi.Controllers
{
    /// <summary>
    /// A Controller for Player actions for the Front End
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("frontend/[controller]")]
    public class PlayersController : Controller
    {
        private readonly PlayersService _playersService;

        public PlayersController(PlayersService playersService)
        {
            _playersService = playersService;
        }

        #region CREATE
        [HttpGet("Create"), Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("Create"), Route("details")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Player player)
        {
            await _playersService.CreateAsync(player);
            return RedirectToAction("IndexAdmin");
        }
        #endregion CREATE

        #region READ
        // Show list of all players with CRUD options for admin
        [HttpGet("Admin"), Route("index")]
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _playersService.GetAsync());
        }

        // Show list of all players ordered by Rank ascending
        [HttpGet("Index"), Route("index")]
        public async Task<IActionResult> Index()
        {
            return View(await _playersService.GetRankedListAsync());
        }

        // Show a single player's details
        [HttpGet("PlayerDetails"), Route("details")]
        public async Task<IActionResult> Details(string id)
        {
            var player = await _playersService.GetAsyncId(id);

            if (player is null)
            {
                return NotFound();
            }

            return View(player);
        }
        #endregion READ

        #region UPDATE
        [HttpGet("Edit"), Route("edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var player = await _playersService.GetAsyncId(id);
            if (player is null)
                return NotFound();

            return View(player);
        }

        [HttpPost("Edit"), Route("details")]
        public async Task<ActionResult> Edit(Player player)
        {
            await _playersService.UpdatePlayerAsync(player);
            return RedirectToAction("indexAdmin");
        }
        #endregion UPDATE

        #region DELETE
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var player = await _playersService.GetAsyncId(id);

            if (player is null)
            {
                return NotFound();
            }

            return View(player);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(Player player)
        {
            await _playersService.RemoveAsync(player);
            return RedirectToAction("IndexAdmin");
        }
        #endregion DELETE
    }
}
