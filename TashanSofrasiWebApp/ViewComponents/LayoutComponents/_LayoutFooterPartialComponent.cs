using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.ViewComponents.LayoutComponents
{
	public class _LayoutFooterPartialComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
