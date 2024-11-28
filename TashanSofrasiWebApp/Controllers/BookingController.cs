using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.BookingDTOs;

namespace TashanSofrasiWebApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public BookingController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
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
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ApiMessage"] = "Rezervasyon işlemi yapılamadı, lütfen tekrar deneyin!";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }

        }
    }
}
