using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
