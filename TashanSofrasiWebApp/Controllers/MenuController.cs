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
        private readonly IConfiguration _configuration;

        public MenuController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;

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
                MenuTableID = Convert.ToInt32(Request.Cookies["MenuTableID"])
            };
            var client = _clientFactory.CreateClient("Default");
            var jsonData = JsonConvert.SerializeObject(createBasketDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Basket", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
