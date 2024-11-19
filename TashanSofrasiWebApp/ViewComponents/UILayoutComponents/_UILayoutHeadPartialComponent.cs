using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.ViewComponents.UILayoutComponents
{
    public class _UILayoutHeadPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
