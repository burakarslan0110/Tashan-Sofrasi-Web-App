using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.AboutDTOs;
using TashanSofrasiWebApp.DTOs.FooterDTOs;
namespace TashanSofrasiWebApp.ViewComponents.HomeComponents
{
    public class _HomeAboutPartialComponent : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public _HomeAboutPartialComponent(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _clientFactory.CreateClient("Default");
            var response = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/About/1");
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
