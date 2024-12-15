using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.AboutDTOs;
using TashanSofrasiWebApp.DTOs.ContactDTOs;

namespace TashanSofrasiWebApp.Controllers
{
	[AllowAnonymous]
	public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AboutController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Default");
            var response = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/About");
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
            var client = _httpClientFactory.CreateClient("Default");
            var content = new StringContent(JsonConvert.SerializeObject(createContactDTO), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Contact", content);
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
