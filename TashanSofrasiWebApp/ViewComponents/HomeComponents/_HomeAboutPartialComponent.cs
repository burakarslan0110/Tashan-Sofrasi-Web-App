using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.AboutDTOs;
using TashanSofrasiWebApp.DTOs.FooterDTOs;
namespace TashanSofrasiWebApp.ViewComponents.HomeComponents
{
    public class _HomeAboutPartialComponent : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public _HomeAboutPartialComponent(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7053/api/About/1");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultAboutDTO>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
