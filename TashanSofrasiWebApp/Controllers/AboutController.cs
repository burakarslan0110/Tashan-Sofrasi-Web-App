using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.AboutDTOs;
using TashanSofrasiWebApp.DTOs.ContactDTOs;

namespace TashanSofrasiWebApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7053/api/About");
            if(response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDTO>>(responseContent);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateContactDTO createContactDTO)
        {
            createContactDTO.ContactDate = DateTime.Now.ToShortDateString();
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(createContactDTO), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7053/api/Contact", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TempData["ApiMessage1"] = responseContent;
                TempData["MessageType1"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ApiMessage1"] = "Mesaj gönderilemedi, lütfen tekrar deneyin!";
                TempData["MessageType1"] = "error";
                return RedirectToAction("Index");
            }
        }
    }
}
