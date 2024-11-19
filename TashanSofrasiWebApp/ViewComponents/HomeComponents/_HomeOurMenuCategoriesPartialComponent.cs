using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.CategoryDTOs;

namespace TashanSofrasiWebApp.ViewComponents.HomeComponents
{
    public class _HomeOurMenuCategoriesPartialComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomeOurMenuCategoriesPartialComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7053/api/Category/");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
