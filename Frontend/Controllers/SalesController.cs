using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
