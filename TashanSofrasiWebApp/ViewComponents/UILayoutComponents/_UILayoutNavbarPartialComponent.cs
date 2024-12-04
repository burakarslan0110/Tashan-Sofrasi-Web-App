using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.FeatureDTOs;

namespace TashanSofrasiWebApp.ViewComponents.UILayoutComponents
{
    public class _UILayoutNavbarPartialComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

		public _UILayoutNavbarPartialComponent(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7053/api/Feature/1");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateFeatureDTO>(jsonData);
				return View(values);
			}
			return View();
        }
    }
}
