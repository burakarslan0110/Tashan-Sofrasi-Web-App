using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.ProductDTOs;

namespace TashanSofrasiWebApp.Controllers
{
    public class PageLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
