using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.AboutDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7053/api/About/1");
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
				var validImageTypes = new[] { "image/jpeg", "image/png", "image/webp" };
				if (!validImageTypes.Contains(updateAboutDTO.AboutImage.ContentType))
				{
					ModelState.AddModelError("AboutImage", "Sadece JPEG, PNG veya WebP formatında görseller kabul edilmektedir.");
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
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateAboutDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7053/api/About/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
