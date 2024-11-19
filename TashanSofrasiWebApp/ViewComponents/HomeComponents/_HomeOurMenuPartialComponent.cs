using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.ProductDTOs;

namespace TashanSofrasiWebApp.ViewComponents.HomeComponents
{
    public class _HomeOurMenuPartialComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomeOurMenuPartialComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7053/api/Product/ListProductWithCategory");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDTO>>(jsonData);
                return View(values);    
            }
            return View();
        }
    }
}
