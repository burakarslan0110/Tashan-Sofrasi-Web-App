using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.BookingDTOs;

namespace TashanSofrasiWebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public HomeController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;

        }

        public IActionResult Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,                
                    SameSite = SameSiteMode.None,   
                    Secure = true,                  
                    Expires = DateTime.UtcNow.AddHours(1) 
                };

                Response.Cookies.Append("MenuTableID", id, cookieOptions);

                return RedirectToAction("Index");
            }
            var menuTableId = Request.Cookies["MenuTableID"];

            ViewBag.MenuTableID = Convert.ToInt32(menuTableId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDTO createBookingDTO)
        {
            var client = _clientFactory.CreateClient("Default");
            var jsonData = JsonConvert.SerializeObject(createBookingDTO);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Booking", stringContent);
            var apiResponseContent = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["ApiMessage"] = apiResponseContent;
                TempData["MessageType"] = "success"; 
                return RedirectToAction("");
            }
            else
            {
                TempData["ApiMessage"] = "Rezervasyon işlemi yapılamadı, lütfen tekrar deneyin!";
                TempData["MessageType"] = "error"; 
                return RedirectToAction("");
            }
           
        }
    }
}
