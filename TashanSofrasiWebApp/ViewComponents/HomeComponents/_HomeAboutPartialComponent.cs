using Microsoft.AspNetCore.Mvc;
namespace TashanSofrasiWebApp.ViewComponents.HomeComponents
{
    public class _HomeAboutPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
