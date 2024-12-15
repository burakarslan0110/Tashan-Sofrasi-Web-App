using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.Areas.Admin.Models;
using TashanSofrasiWebApp.DTOs.DiscountDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public DiscountController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Discount");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultDiscountDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> UpdateDiscount(int id)
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Discount/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateDiscountDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDiscount(UpdateDiscountDTO updateDiscountDTO)
        {
            if (updateDiscountDTO.DiscountImage != null)
            {
				var validImageTypes = new[] { "image/jpeg", "image/png", "image/webp" };
				if (!validImageTypes.Contains(updateDiscountDTO.DiscountImage.ContentType))
				{
					ModelState.AddModelError("DiscountImage", "Sadece JPEG, PNG veya WebP formatında görseller kabul edilmektedir.");
					return View(updateDiscountDTO);
				}
				// Dosyayı sunucuda bir dizine kaydet
				string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/discounts");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + updateDiscountDTO.DiscountImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await updateDiscountDTO.DiscountImage.CopyToAsync(fileStream);
                }

                // Görsel yolunu DTO'ya ekle
                updateDiscountDTO.DiscountImageURL = $"/images/discounts/{uniqueFileName}";
            }
            updateDiscountDTO.DiscountStatus = true;
			var client = _httpClientFactory.CreateClient("Default");
            var jsonData = JsonConvert.SerializeObject(updateDiscountDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Discount/", stringContent);
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
                return View(updateDiscountDTO);
            }
            return View();
        }

        public async Task<IActionResult> ChangeDiscountStatusToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Discount/ChangeDiscountStatusToTrue/{id}", null);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
        }

		public async Task<IActionResult> ChangeDiscountStatusToFalse(int id)
		{
			var client = _httpClientFactory.CreateClient("Default");
			var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Discount/ChangeDiscountStatusToFalse/{id}", null);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

	}
}
