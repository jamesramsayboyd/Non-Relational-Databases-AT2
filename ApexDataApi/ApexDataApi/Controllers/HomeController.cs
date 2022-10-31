using Microsoft.AspNetCore.Mvc;

namespace ApexDataApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
