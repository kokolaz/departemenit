using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Diagram()
        {
            return View();
        }
    }
}
