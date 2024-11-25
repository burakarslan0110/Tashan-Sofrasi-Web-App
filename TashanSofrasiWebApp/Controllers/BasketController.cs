using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.BasketDTOs;

namespace TashanSofrasiWebApp.Controllers
{
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
            var response = await client.GetAsync("https://localhost:7053/api/Basket?id=4");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBasketDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
