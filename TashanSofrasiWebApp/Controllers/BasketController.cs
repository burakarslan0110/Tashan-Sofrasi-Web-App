using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.BasketDTOs;

namespace TashanSofrasiWebApp.Controllers
{
	[AllowAnonymous]
	public class BasketController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public BasketController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;

        }

        public async Task<IActionResult> Index()
        {
            var cookieValue = Request.Cookies["MenuTableID"];
            int id = Convert.ToInt32(cookieValue);
            var client = _clientFactory.CreateClient("Default");
            var response = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Basket?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBasketDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> DeleteBasket(int id)
        {
           var client = _clientFactory.CreateClient("Default");
            var response = await client.DeleteAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Basket/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
           return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder([FromForm] int menuTableId)
        {
            if (menuTableId <= 0)
            {
                return BadRequest("Geçerli bir menuTableId gönderilmedi.");
            }

            var client = _clientFactory.CreateClient("Default");
            var response = await client.PostAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Basket/CompleteOrder/{menuTableId}", null);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Sipariş başarıyla tamamlandı!");
            }

            return StatusCode(500, "Sipariş tamamlanamadı.");
        }



    }
}
