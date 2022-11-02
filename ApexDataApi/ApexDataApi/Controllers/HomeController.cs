using Microsoft.AspNetCore.Mvc;

namespace ApexDataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("Home"), Route("index"), ApiExplorerSettings(IgnoreApi = true)]
        public string Index()
        {
            return "Success";
        }

        //[Route("index")]
        //[HttpGet("Index")]
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}
