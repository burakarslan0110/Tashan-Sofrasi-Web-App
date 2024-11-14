using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.ViewComponents.LayoutComponents
{
	public class _LayoutScriptPartialComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
