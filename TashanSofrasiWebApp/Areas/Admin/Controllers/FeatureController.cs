using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.Areas.Admin.Models;
using TashanSofrasiWebApp.DTOs.FeatureDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FeatureController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Feature/1");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFeatureDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateFeatureDTO updateFeatureDTO)
        {
			if (updateFeatureDTO.FeatureBackgroundImage != null)
			{
				var validImageTypes = new[] { "image/jpeg", "image/png", "image/webp" };
				if (!validImageTypes.Contains(updateFeatureDTO.FeatureBackgroundImage.ContentType))
				{
					ModelState.AddModelError("FeatureBackgroundImage", "Sadece JPEG, PNG veya WebP formatında görseller kabul edilmektedir.");
					return View(updateFeatureDTO);
				}
				// Dosyayı sunucuda bir dizine kaydet
				string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/background");
				if (!Directory.Exists(uploadsFolder))
					Directory.CreateDirectory(uploadsFolder);

				string uniqueFileName = Guid.NewGuid().ToString() + "_" + updateFeatureDTO.FeatureBackgroundImage.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await updateFeatureDTO.FeatureBackgroundImage.CopyToAsync(fileStream);
				}

				// Görsel yolunu DTO'ya ekle
				updateFeatureDTO.FeatureBackgroundImageURL = $"/images/background/{uniqueFileName}";
			}
			updateFeatureDTO.FeatureID = 1;
            var client = _httpClientFactory.CreateClient("Default");
            var jsonData = JsonConvert.SerializeObject(updateFeatureDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Feature/", stringContent);
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
			}
            return View(updateFeatureDTO);
        }
    }
}
