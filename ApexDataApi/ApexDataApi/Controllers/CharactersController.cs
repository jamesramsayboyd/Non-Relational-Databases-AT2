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
    // GET: Characters
    //public async Task<IActionResult> Index()
    //{
    //    return View(await _context.Character.ToListAsync());
    //}

    //// GET: Characters/Details/5
    //public async Task<IActionResult> Details(string id)
    //{
    //    if (id == null || _context.Character == null)
    //    {
    //        return NotFound();
    //    }

    //    var character = await _context.Character
    //        .FirstOrDefaultAsync(m => m.Id == id);
    //    if (character == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(character);
    //}

    //// GET: Characters/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: Characters/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("Id,CharacterName,Playtime")] Character character)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(character);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(character);
    //}

    //// GET: Characters/Edit/5
    //public async Task<IActionResult> Edit(string id)
    //{
    //    if (id == null || _context.Character == null)
    //    {
    //        return NotFound();
    //    }

    //    var character = await _context.Character.FindAsync(id);
    //    if (character == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(character);
    //}

    //// POST: Characters/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(string id, [Bind("Id,CharacterName,Playtime")] Character character)
    //{
    //    if (id != character.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(character);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!CharacterExists(character.Id))
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
    //    return View(character);
    //}

    //// GET: Characters/Delete/5
    //public async Task<IActionResult> Delete(string id)
    //{
    //    if (id == null || _context.Character == null)
    //    {
    //        return NotFound();
    //    }

    //    var character = await _context.Character
    //        .FirstOrDefaultAsync(m => m.Id == id);
    //    if (character == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(character);
    //}

    //// POST: Characters/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(string id)
    //{
    //    if (_context.Character == null)
    //    {
    //        return Problem("Entity set 'ApexDataApiContext.Character'  is null.");
    //    }
    //    var character = await _context.Character.FindAsync(id);
    //    if (character != null)
    //    {
    //        _context.Character.Remove(character);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool CharacterExists(string id)
    //{
    //  return _context.Character.Any(e => e.Id == id);
    //}
    #endregion default stuff

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    public class CharactersController : Controller
    {        
        private readonly CharactersService _charactersService;

        public CharactersController(CharactersService charactersService)
        {
            _charactersService = charactersService;
        }

        // Show list of all characters with CRUD options
        [HttpGet("Admin"), Route("index"), ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _charactersService.GetAsync());
        }

        // Show list of all characters, no CRUD
        [HttpGet("Index"), Route("index"), ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Index()
        {
            return View(await _charactersService.GetAsync());
        }
    }
}
