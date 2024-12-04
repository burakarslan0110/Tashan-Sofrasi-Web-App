using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.TestimonialDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7053/api/Testimonial");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTestimonialDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDTO createTestimonialDTO)
        {
            if (createTestimonialDTO.TestimonialImage != null)
            {
                var validImageTypes = new[] { "image/jpeg", "image/png", "image/webp" };
                if (!validImageTypes.Contains(createTestimonialDTO.TestimonialImage.ContentType))
                {
                    ModelState.AddModelError("AboutImage", "Sadece JPEG, PNG veya WebP formatında görseller kabul edilmektedir.");
                    return View(createTestimonialDTO);
                }
                // Dosyayı sunucuda bir dizine kaydet
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/testimonials");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + createTestimonialDTO.TestimonialImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await createTestimonialDTO.TestimonialImage.CopyToAsync(fileStream);
                }

                // Görsel yolunu DTO'ya ekle
                createTestimonialDTO.TestimonialImageURL = $"/images/testimonials/{uniqueFileName}";
            }
            createTestimonialDTO.TestimonialStatus = true;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createTestimonialDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7053/api/Testimonial", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7053/api/Testimonial/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7053/api/Testimonial/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateTestimonialDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDTO updateTestimonialDTO)
        {
            if (updateTestimonialDTO.TestimonialImage != null)
            {
                var validImageTypes = new[] { "image/jpeg", "image/png", "image/webp" };
                if (!validImageTypes.Contains(updateTestimonialDTO.TestimonialImage.ContentType))
                {
                    ModelState.AddModelError("AboutImage", "Sadece JPEG, PNG veya WebP formatında görseller kabul edilmektedir.");
                    return View(updateTestimonialDTO);
                }
                // Dosyayı sunucuda bir dizine kaydet
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/testimonials");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + updateTestimonialDTO.TestimonialImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await updateTestimonialDTO.TestimonialImage.CopyToAsync(fileStream);
                }

                // Görsel yolunu DTO'ya ekle
                updateTestimonialDTO.TestimonialImageURL = $"/images/testimonials/{uniqueFileName}";
            }
            updateTestimonialDTO.TestimonialStatus = true;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateTestimonialDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7053/api/Testimonial", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ChangeTestimonialStatusToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsync($"https://localhost:7053/api/Testimonial/ChangeTestimonialStatusToFalse/{id}", null);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ChangeTestimonialStatusToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsync($"https://localhost:7053/api/Testimonial/ChangeTestimonialStatusToTrue/{id}", null);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
