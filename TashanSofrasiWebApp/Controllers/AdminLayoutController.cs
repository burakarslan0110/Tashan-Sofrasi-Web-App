using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
