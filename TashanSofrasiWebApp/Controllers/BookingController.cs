using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.BookingDTOs;

namespace TashanSofrasiWebApp.Controllers
{
	[AllowAnonymous]
	public class BookingController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public BookingController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookingByCustomer(CreateBookingDTO createBookingDTO)
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
