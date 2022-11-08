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
    //[ApiController]
    [Route("api/[controller]")]
    public class CharactersController : Controller
    {        
        private readonly CharactersService _charactersService;

        public CharactersController(CharactersService charactersService)
        {
            _charactersService = charactersService;
        }

        #region FRONT END
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

        #endregion FRONT END

        //#region API
        //#region SELECT ALL CHARACTERS
        ///// <summary>
        ///// Selects all Characters
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("AllCharacters")]
        //public async Task<List<Character>> GetCharacters() =>
        //    await _charactersService.GetAsync();
        //#endregion SELECT ALL CHARACTERS

        //#region SORT CHARACTERS
        ///// <summary>
        ///// Sorts all Characters by Playtime
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("CharactersByPlaytime")]
        //public async Task<List<Character>> GetCharactersByPlaytime() =>
        //    await _charactersService.GetPlaytimeAsync();
        //#endregion SORT CHARACTERS

        //#region INSERT CHARACTERS
        ///// <summary>
        ///// Inserts two Characters in one operation
        ///// </summary>
        ///// <param name="characterList">A List of Character objects serialized from the request body</param>
        ///// <returns></returns>
        //[HttpPost("{name1}/{playtime1}/{name2}/{playtime2}")]
        //public async Task<IActionResult> PostTwo(string name1, int playtime1, string name2, int playtime2)
        //{
        //    await _charactersService.CreateListAsync(name1, playtime1, name2, playtime2);

        //    return CreatedAtAction(nameof(GetCharacters), 0, 0);
        //}
        //#endregion INSERT CHARACTERS

        //#region DELETE CHARACTERS
        ///// <summary>
        ///// Deletes a specific Character
        ///// </summary>
        ///// <param name="name">Name of the character to delete (case-insensitive)</param>
        ///// <returns></returns>
        //[HttpDelete("{name}")]
        //public async Task<IActionResult> Delete(string name)
        //{
        //    var character = await _charactersService.GetAsync(name);

        //    if (character is null)
        //    {
        //        return NotFound();
        //    }

        //    await _charactersService.RemoveAsync(name);

        //    return NoContent();
        //}

        ///// <summary>
        ///// Deletes multiple Characters in one operation
        ///// </summary>
        ///// <param name="name1">Name of the first character to delete</param>
        ///// <param name="name2">Name of the second character to delete</param>
        ///// <returns></returns>
        //[HttpDelete("{name1}/{name2}")]
        //public async Task<IActionResult> Delete(string name1, string name2)
        //{
        //    var character = await _charactersService.GetAsync(name1);

        //    if (character is null)
        //        return NotFound();

        //    var character2 = await _charactersService.GetAsync(name2);

        //    if (character2 is null)
        //        return NotFound();

        //    await _charactersService.RemoveAsync(name1);
        //    await _charactersService.RemoveAsync(name2);

        //    return NoContent();
        //}
        //#endregion DELETE CHARACTERS
        //#endregion API
    }
}
