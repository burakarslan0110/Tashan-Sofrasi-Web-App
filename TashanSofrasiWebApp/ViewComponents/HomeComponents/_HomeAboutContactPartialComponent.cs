using Microsoft.AspNetCore.Mvc;
using TashanSofrasiWebApp.DTOs.ContactDTOs;

namespace TashanSofrasiWebApp.ViewComponents.HomeComponents
{
    public class _HomeAboutContactPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var createContactDTO = new CreateContactDTO();
            return View(createContactDTO);
        }
    }
}
