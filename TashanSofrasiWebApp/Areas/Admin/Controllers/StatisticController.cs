using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    public class StatisticController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
