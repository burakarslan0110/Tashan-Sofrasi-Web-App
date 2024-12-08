using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.BookingDTOs;
using TashanSofrasiWebApp.Models;

namespace TashanSofrasiWebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                // Cookie'yi oluşturup istemciye gönderiyoruz
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,                // Tarayıcıdan erişim kısıtlanır
                    SameSite = SameSiteMode.None,   // Cross-Origin desteği için gerekli
                    Secure = true,                  // HTTPS kullanılıyorsa
                    Expires = DateTime.UtcNow.AddHours(1) // Cookie'nin geçerlilik süresi
                };

                Response.Cookies.Append("MenuTableID", id, cookieOptions);

                // URL'deki parametreyi temizlemek için Redirect yapıyoruz
                return RedirectToAction("Index");
            }
            // Cookie'deki MenuTableID'yi alıyoruz
            var menuTableId = Request.Cookies["MenuTableID"];

            // View'e MenuTableID'yi gönderiyoruz (isteğe bağlı)
            ViewBag.MenuTableID = Convert.ToInt32(menuTableId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDTO createBookingDTO)
        {
            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDTO);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7053/api/Booking", stringContent);
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
