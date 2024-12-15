using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasi.EntityLayer.Entities;
using TashanSofrasiWebApp.DTOs.NotificationDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public NotificationController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        [HttpGet]
        public async Task<IActionResult> NotificationRead()
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Notification/NotificationRead");
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return NoContent(); 
        }
    }
}
