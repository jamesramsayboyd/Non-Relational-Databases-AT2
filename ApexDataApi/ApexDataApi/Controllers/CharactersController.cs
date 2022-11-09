using Microsoft.AspNetCore.Mvc;
using ApexDataApi.Services;
using ApexDataApi.Models;

namespace ApexDataApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("frontend/[controller]")]
    public class CharactersController : Controller
    {        
        private readonly CharactersService _charactersService;

        public CharactersController(CharactersService charactersService)
        {
            _charactersService = charactersService;
        }

        #region CREATE
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Character character)
        {
            await _charactersService.CreateAsync(character);
            return RedirectToAction("IndexAdmin");
        }
        #endregion CREATE

        #region READ
        // Show list of all characters with CRUD options
        [HttpGet("Admin"), Route("index")]
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _charactersService.GetCharacterList());
        }

        // Show list of all characters, no CRUD
        [HttpGet("Index"), Route("index")]
        public async Task<IActionResult> Index()
        {
            return View(await _charactersService.GetCharacterListRanked());
        }

        // Show a single character's details
        [HttpGet("CharacterDetails"), Route("details")]
        public async Task<IActionResult> Details(string id)
        {
            var character = await _charactersService.GetAsyncId(id);

            if (character is null)
            {
                return NotFound();
            }

            return View(character);
        }
        #endregion READ

        #region UPDATE
        [HttpGet("Edit"), Route("edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var character = await _charactersService.GetAsyncId(id);
            if (character is null)
                return NotFound();

            return View(character);
        }

        [HttpPost("Edit"), Route("details")]
        public async Task<ActionResult> Edit(Character character)
        {
            await _charactersService.UpdateCharacterAsync(character);
            return RedirectToAction("indexAdmin");
        }
        #endregion UPDATE

        #region DELETE
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var character = await _charactersService.GetAsyncId(id);

            if (character is null)
            {
                return NotFound();
            }

            return View(character);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(Character character)
        {
            await _charactersService.RemoveAsync(character);
            return RedirectToAction("IndexAdmin");
        }
        #endregion DELETE

        
    }
}
