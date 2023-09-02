using Microsoft.AspNetCore.Mvc;

namespace StorePro.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult BadRequest()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
