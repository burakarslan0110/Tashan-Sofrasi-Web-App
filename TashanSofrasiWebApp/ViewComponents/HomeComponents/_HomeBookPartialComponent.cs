 using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.ViewComponents.HomeComponents
{
    public class _HomeBookPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
