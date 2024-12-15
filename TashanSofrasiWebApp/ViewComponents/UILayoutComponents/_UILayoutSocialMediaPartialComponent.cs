using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.SocialMediaDTOs;

namespace TashanSofrasiWebApp.ViewComponents.UILayoutComponents
{
    public class _UILayoutSocialMediaPartialComponent : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public _UILayoutSocialMediaPartialComponent(IHttpClientFactory clientFactory , IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _clientFactory.CreateClient("Default");
            var response = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/SocialMedia/");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var socialMediaDTO = JsonConvert.DeserializeObject<List<GetSocialMediaDTO>>(jsonData);
                return View(socialMediaDTO);
            }
            return View();
        }
    }
}
