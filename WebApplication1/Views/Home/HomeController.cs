using Microsoft.AspNetCore.Mvc;

namespace StorePro.Views.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
