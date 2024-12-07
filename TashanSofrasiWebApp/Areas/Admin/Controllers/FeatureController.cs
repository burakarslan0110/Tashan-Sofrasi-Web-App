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

        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7053/api/Feature/1");
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
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7053/api/Feature/", stringContent);
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
