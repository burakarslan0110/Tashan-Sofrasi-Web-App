using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.BasketDTOs;

namespace TashanSofrasiWebApp.Controllers
{
	[AllowAnonymous]
	public class MenuController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public MenuController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(int id, int count)
        {
            CreateBasketDTO createBasketDTO = new CreateBasketDTO
            {
                ProductID = id,
                Count = count,
            };
            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBasketDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7053/api/Basket", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
