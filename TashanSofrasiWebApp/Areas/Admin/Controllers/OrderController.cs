using Microsoft.AspNetCore.Mvc;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public OrderController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            var client = _clientFactory.CreateClient();
            var responseMessage = await client.PutAsync($"https://localhost:7053/api/Order/CompleteOrder/{id}", null);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","MenuTable");
            }
            return RedirectToAction("Index");

        }
    }
}
