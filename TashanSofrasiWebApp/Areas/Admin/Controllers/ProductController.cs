using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using TashanSofrasiWebApp.DTOs.CategoryDTOs;
using TashanSofrasiWebApp.DTOs.ProductDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProductController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Product/ListProductWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Category");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData);
			List<SelectListItem> valuespick = values.Select(x => new SelectListItem
			{
				Text = x.CategoryName,
				Value = x.CategoryID.ToString()
			}).ToList();
			ViewBag.valuespickcategory = valuespick;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
			if (createProductDTO.ProductImage != null)
			{
                var validImageTypes = new[] { "image/jpeg", "image/png", "image/webp" };
                if (!validImageTypes.Contains(createProductDTO.ProductImage.ContentType))
                {
                    ModelState.AddModelError("AboutImage", "Sadece JPEG, PNG veya WebP formatında görseller kabul edilmektedir.");
                    return View(createProductDTO);
                }
                // Dosyayı sunucuda bir dizine kaydet
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");
				if (!Directory.Exists(uploadsFolder))
					Directory.CreateDirectory(uploadsFolder);

				string uniqueFileName = Guid.NewGuid().ToString() + "_" + createProductDTO.ProductImage.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await createProductDTO.ProductImage.CopyToAsync(fileStream);
				}

				// Görsel yolunu DTO'ya ekle
				createProductDTO.ProductImageURL = $"/images/products/{uniqueFileName}";
			}

			createProductDTO.ProductStatus = true;
            var client = _httpClientFactory.CreateClient("Default");
            var jsonData = JsonConvert.SerializeObject(createProductDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Product", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.DeleteAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client1 = _httpClientFactory.CreateClient("Default");
            var responseMessage1 = await client1.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Category");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData1);
			List<SelectListItem> valuespick = values1.Select(x => new SelectListItem
			{
				Text = x.CategoryName,
				Value = x.CategoryID.ToString()
			}).ToList();

			ViewBag.valuespickcategory = valuespick;

            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
			if (updateProductDTO.ProductImage != null)
			{
                var validImageTypes = new[] { "image/jpeg", "image/png", "image/webp" };
                if (!validImageTypes.Contains(updateProductDTO.ProductImage.ContentType))
                {
                    ModelState.AddModelError("AboutImage", "Sadece JPEG, PNG veya WebP formatında görseller kabul edilmektedir.");
                    return View(updateProductDTO);
                }
                // Dosyayı sunucuda bir dizine kaydet
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");
				if (!Directory.Exists(uploadsFolder))
					Directory.CreateDirectory(uploadsFolder);

				string uniqueFileName = Guid.NewGuid().ToString() + "_" + updateProductDTO.ProductImage.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await updateProductDTO.ProductImage.CopyToAsync(fileStream);
				}

				// Görsel yolunu DTO'ya ekle
				updateProductDTO.ProductImageURL = $"/images/products/{uniqueFileName}";
			}
			updateProductDTO.ProductStatus = true;
            var client = _httpClientFactory.CreateClient("Default");
            var jsonData = JsonConvert.SerializeObject(updateProductDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responeMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Product/", stringContent);
            if (responeMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ChangeProductStatusToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Product/ChangeProductStatusToTrue/{id}",null);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }

        public async Task<IActionResult> ChangeProductStatusToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Product/ChangeProductStatusToFalse/{id}", null);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
