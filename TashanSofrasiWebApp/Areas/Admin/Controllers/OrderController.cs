using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public OrderController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;

        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            var client = _clientFactory.CreateClient("Default");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Order/CompleteOrder/{id}", null);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","MenuTable");
            }
            return RedirectToAction("Index");

        }
    }
}
