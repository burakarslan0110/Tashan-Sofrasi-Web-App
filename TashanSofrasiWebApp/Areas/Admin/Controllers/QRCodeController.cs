using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using TashanSofrasiWebApp.DTOs.MenuTableDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QRCodeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public QRCodeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // API'den tüm masa bilgilerini alıyoruz
            var client = _clientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7053/api/MenuTable");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMenuTableDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(int menuTableID)
        {
            string baseUrl = "https://127.0.0.1:7114/Home/Index"; // Ana sayfanın URL'si
            string fullUrl = $"{baseUrl}?id={menuTableID}";
            using (MemoryStream memoryStream = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(fullUrl, QRCodeGenerator.ECCLevel.Q);

                using (Bitmap image = qrCode.GetGraphic(20))
                {
                    image.Save(memoryStream, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
                var client = _clientFactory.CreateClient();
                var responseMessage = client.GetAsync("https://localhost:7053/api/MenuTable").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = responseMessage.Content.ReadAsStringAsync().Result;
                    var values = JsonConvert.DeserializeObject<List<ResultMenuTableDTO>>(jsonData);
                    return View(values);
                }

                return View();
            }
        }
    }
}
