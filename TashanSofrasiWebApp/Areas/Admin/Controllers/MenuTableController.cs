using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TashanSofrasiWebApp.DTOs.OrderDetailDTOs;
using TashanSofrasiWebApp.DTOs.MenuTableDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuTableController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public MenuTableController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;

        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("Default");
            var responseMessage = client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/MenuTable");
            if (responseMessage.Result.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Result.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMenuTableDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateMenuTable()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuTable(CreateMenuTableDTO createMenuTableDTO)
        {
            createMenuTableDTO.MenuTableStatus = true;
            var client = _clientFactory.CreateClient("Default");
            var jsonData = JsonConvert.SerializeObject(createMenuTableDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/MenuTable", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteMenuTable(int id)
        {
            var client = _clientFactory.CreateClient("Default");
            var responseMessage = await client.DeleteAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/MenuTable/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MenuTableStats(int id)
        {
            var client = _clientFactory.CreateClient("Default");
            var responseMenuTable = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/MenuTable/{id}");
            UpdateMenuTableDTO updateMenuTableDTO = null;
            if (responseMenuTable.IsSuccessStatusCode)
            {
                var jsonData = await responseMenuTable.Content.ReadAsStringAsync();
                updateMenuTableDTO = JsonConvert.DeserializeObject<UpdateMenuTableDTO>(jsonData);
            }


            var responseOrderDetail = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/OrderDetail/{id}");
            List<OrderDetailDTO> orderDetailDTOs = null;
            if (responseOrderDetail.IsSuccessStatusCode)
            {
                var jsonData = await responseOrderDetail.Content.ReadAsStringAsync();
                orderDetailDTOs = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(jsonData);
            }

            var menuTableStats = new MenuTableStatsDTO
            {
                orderDetailDTO = orderDetailDTOs,
                updateMenuTableDTO = updateMenuTableDTO
            };
            return View(menuTableStats);
        }

        [HttpPost]
        public async Task<IActionResult> MenuTableStats(MenuTableStatsDTO menuTableStatsDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(menuTableStatsDTO);
            }

            var client = _clientFactory.CreateClient("Default");
            menuTableStatsDTO.updateMenuTableDTO.MenuTableStatus = true;
            var menuTableJson = JsonConvert.SerializeObject(menuTableStatsDTO.updateMenuTableDTO);
            StringContent menuTableContent = new StringContent(menuTableJson, Encoding.UTF8, "application/json");
            var menuTableResponse = await client.PutAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/MenuTable", menuTableContent);

            if (menuTableResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(menuTableStatsDTO);
        }

        
    }
}
