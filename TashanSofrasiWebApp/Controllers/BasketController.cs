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

        public BasketController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7053/api/Basket?id=1");
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
           var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7053/api/Basket/{id}");
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

            var client = _clientFactory.CreateClient();
            var response = await client.PostAsync($"https://localhost:7053/api/Basket/CompleteOrder/{menuTableId}", null);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Sipariş başarıyla tamamlandı!");
            }

            return StatusCode(500, "Sipariş tamamlanamadı.");
        }



    }
}
