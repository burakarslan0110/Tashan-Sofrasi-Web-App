using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.Areas.Admin.Models;
using TashanSofrasiWebApp.DTOs.AboutDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AboutController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/About/1");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateAboutDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateAboutDTO updateAboutDTO)
        {
            if (updateAboutDTO.AboutImage != null)
            {
				if (updateAboutDTO.AboutImage.ContentType != "image/jpeg" && updateAboutDTO.AboutImage.ContentType != "image/png" && updateAboutDTO.AboutImage.ContentType != "image/webp")
				{
					ModelState.AddModelError("AboutImage", "Hakkımda görseli sadece JPEG, PNG veya WebP formatında olabilir.");
					return View(updateAboutDTO);
				}
				// Dosyayı sunucuda bir dizine kaydet
				string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + updateAboutDTO.AboutImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await updateAboutDTO.AboutImage.CopyToAsync(fileStream);
                }

                // Görsel yolunu DTO'ya ekle
                updateAboutDTO.AboutImageURL = $"/images/about/{uniqueFileName}";
            }
            updateAboutDTO.AboutID = 1;
            var client = _httpClientFactory.CreateClient("Default");
            var jsonData = JsonConvert.SerializeObject(updateAboutDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/About/", stringContent);
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
				return View(updateAboutDTO);
			}
   
        }
    }
}
