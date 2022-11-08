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
    [Authorize]
    [ApiController]
    [Route("api/Characters")]
    public class CharactersApiController : Controller
    {
        private readonly CharactersService _charactersService;

        public CharactersApiController(CharactersService charactersService)
        {
            _charactersService = charactersService;
        }

        #region SELECT ALL CHARACTERS
        /// <summary>
        /// Selects all Characters
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllCharacters")]
        public async Task<List<Character>> GetCharacters() =>
            await _charactersService.GetCharacterList();
        #endregion SELECT ALL CHARACTERS

        #region SORT CHARACTERS
        /// <summary>
        /// Sorts all Characters by Playtime
        /// </summary>
        /// <returns></returns>
        [HttpGet("CharactersByPlaytime")]
        public async Task<List<Character>> GetCharactersByPlaytime() =>
            await _charactersService.GetCharacterListRanked();
        #endregion SORT CHARACTERS

        #region INSERT CHARACTERS
        /// <summary>
        /// Inserts two Characters in one operation
        /// </summary>
        /// <param name="characterList">A List of Character objects serialized from the request body</param>
        /// <returns></returns>
        [HttpPost("{name1}/{playtime1}/{name2}/{playtime2}")]
        public async Task<IActionResult> PostTwo(string name1, int playtime1, string name2, int playtime2)
        {
            await _charactersService.CreateListAsync(name1, playtime1, name2, playtime2);

            return CreatedAtAction(nameof(GetCharacters), 0, 0);
        }
        #endregion INSERT CHARACTERS

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
}
