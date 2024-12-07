﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TashanSofrasiWebApp.DTOs.FooterDTOs;
using TashanSofrasiWebApp.DTOs.TestimonialDTOs;

namespace TashanSofrasiWebApp.ViewComponents.UILayoutComponents
{
    public class _UILayoutFooterPartialComponent : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public _UILayoutFooterPartialComponent(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7053/api/Footer/1");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var footerDTO = JsonConvert.DeserializeObject<ResultFooterDTO>(jsonData);
                return View(footerDTO);
            }
            return View();
        }
    }
}
