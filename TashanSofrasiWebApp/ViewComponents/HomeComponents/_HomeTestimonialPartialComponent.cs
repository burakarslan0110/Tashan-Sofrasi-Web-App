using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.TestimonialDTOs;

namespace TashanSofrasiWebApp.ViewComponents.HomeComponents
{
    public class _HomeTestimonialPartialComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public _HomeTestimonialPartialComponent(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           var client = _httpClientFactory.CreateClient("Default");
           var response = client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Testimonial");
            if (response.Result.IsSuccessStatusCode)
            {
                var content = await response.Result.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTestimonialDTO>>(content);
                return View(values);
            }
            return View();
        }
    }
}
