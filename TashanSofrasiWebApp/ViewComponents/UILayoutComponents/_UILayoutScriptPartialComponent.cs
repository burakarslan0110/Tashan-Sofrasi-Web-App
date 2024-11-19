using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.ViewComponents.UILayoutComponents
{
    public class _UILayoutScriptPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
