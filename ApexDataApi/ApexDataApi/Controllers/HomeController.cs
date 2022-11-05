using Microsoft.AspNetCore.Mvc;

namespace ApexDataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        //[HttpGet("Home"), Route("index"), ApiExplorerSettings(IgnoreApi = true)]
        //public string Index()
        //{
        //    return "Success";
        //}

        [HttpGet("Home"), Route("index"), ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet("HomePublic"), Route("IndexPublic"), ApiExplorerSettings(IgnoreApi = true)]
        //public ActionResult IndexPublic()
        //{
        //    return View();
        //}

        //[HttpGet("Home"), Route("players/index"), ApiExplorerSettings(IgnoreApi = true)]
        //public ActionResult Players()
        //{
        //    return RedirectToAction("Players/Index");
        //}
    }
}
