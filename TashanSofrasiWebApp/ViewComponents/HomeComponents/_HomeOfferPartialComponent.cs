using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.DiscountDTOs;

namespace TashanSofrasiWebApp.ViewComponents.HomeComponents
{
    public class _HomeOfferPartialComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomeOfferPartialComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7053/api/Discount");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultDiscountDTO>>(content);
                return View(values);
            }
            return View();
        }
    }

}
