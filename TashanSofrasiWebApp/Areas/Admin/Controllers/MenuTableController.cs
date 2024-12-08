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

        public MenuTableController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var responseMessage = client.GetAsync("https://localhost:7053/api/MenuTable");
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
            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMenuTableDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7053/api/MenuTable", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteMenuTable(int id)
        {
            var client = _clientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7053/api/MenuTable/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MenuTableStats(int id)
        {
            var client = _clientFactory.CreateClient();
            var responseMenuTable = await client.GetAsync($"https://localhost:7053/api/MenuTable/{id}");
            UpdateMenuTableDTO updateMenuTableDTO = null;
            if (responseMenuTable.IsSuccessStatusCode)
            {
                var jsonData = await responseMenuTable.Content.ReadAsStringAsync();
                updateMenuTableDTO = JsonConvert.DeserializeObject<UpdateMenuTableDTO>(jsonData);
            }


            var responseOrderDetail = await client.GetAsync($"https://localhost:7053/api/OrderDetail/{id}");
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

            var client = _clientFactory.CreateClient();

            menuTableStatsDTO.updateMenuTableDTO.MenuTableStatus = true;
            var menuTableJson = JsonConvert.SerializeObject(menuTableStatsDTO.updateMenuTableDTO);
            StringContent menuTableContent = new StringContent(menuTableJson, Encoding.UTF8, "application/json");
            var menuTableResponse = await client.PutAsync("https://localhost:7053/api/MenuTable", menuTableContent);

            if (menuTableResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(menuTableStatsDTO);
        }

        
    }
}
