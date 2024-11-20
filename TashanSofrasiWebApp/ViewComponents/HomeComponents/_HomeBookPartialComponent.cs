 using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.BookingDTOs;

namespace TashanSofrasiWebApp.ViewComponents.HomeComponents
{
    public class _HomeBookPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new CreateBookingDTO();
            return View(model);
        }
    }
}
