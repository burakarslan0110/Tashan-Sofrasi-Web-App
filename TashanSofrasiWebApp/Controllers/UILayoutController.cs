using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
