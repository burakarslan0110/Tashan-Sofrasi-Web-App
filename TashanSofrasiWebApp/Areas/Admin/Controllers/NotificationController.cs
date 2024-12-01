using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasi.EntityLayer.Entities;
using TashanSofrasiWebApp.DTOs.NotificationDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NotificationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> NotificationRead()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7053/api/Notification/NotificationRead");
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return NoContent(); 
        }
    }
}
