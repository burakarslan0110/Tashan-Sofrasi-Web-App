﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.Areas.Admin.Models;
using TashanSofrasiWebApp.DTOs.CategoryDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CategoryController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Category");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            createCategoryDTO.CategoryStatus = true;
            var client = _httpClientFactory.CreateClient("Default");
            var jsonData = JsonConvert.SerializeObject(createCategoryDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Category", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
				var errorResponse = await responseMessage.Content.ReadFromJsonAsync<ApiValidationErrorResponse>();
				if (errorResponse?.Errors != null)
				{
					foreach (var error in errorResponse.Errors)
					{
						foreach (var errorMessage in error.Value)
						{
							ModelState.AddModelError(error.Key, errorMessage);
						}
					}
				}
				return View(createCategoryDTO);
			}
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.DeleteAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            updateCategoryDTO.CategoryStatus = true;
            var client = _httpClientFactory.CreateClient("Default");
            var jsonData = JsonConvert.SerializeObject(updateCategoryDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Category/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
				var errorResponse = await responseMessage.Content.ReadFromJsonAsync<ApiValidationErrorResponse>();
				if (errorResponse?.Errors != null)
				{
					foreach (var error in errorResponse.Errors)
					{
						foreach (var errorMessage in error.Value)
						{
							ModelState.AddModelError(error.Key, errorMessage);
						}
					}
				}
				return View(updateCategoryDTO);
			}
        }

        public async Task<IActionResult> CategoryStatusChangeToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Category/CategoryStatusChangeToFalse/{id}", null);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }

        public async Task<IActionResult> CategoryStatusChangeToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient("Default");
            var responseMessage = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/Category/CategoryStatusChangeToTrue/{id}", null);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
