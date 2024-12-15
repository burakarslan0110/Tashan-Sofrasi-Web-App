using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.Areas.Admin.Models;
using TashanSofrasiWebApp.DTOs.FooterDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FooterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FooterController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Footer/1");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFooterDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateFooterDTO updateFooterDTO)
        {
            updateFooterDTO.FooterID = 1;
            var client = _httpClientFactory.CreateClient("Default");
            var JsonData = JsonConvert.SerializeObject(updateFooterDTO);
            StringContent stringContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Footer/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
				var errorResponse = await responseMessage.Content.ReadFromJsonAsync<ApiValidationErrorResponse>();
				if (errorResponse?.Errors != null)
				{
					foreach (var error in errorResponse.Errors)
					{
						foreach (var errorMessage in error.Value)
						{
							ModelState.AddModelError(error.Key, errorMessage);
						}
					}
				}
				return View(updateFooterDTO);
			}
        }

    }
}
