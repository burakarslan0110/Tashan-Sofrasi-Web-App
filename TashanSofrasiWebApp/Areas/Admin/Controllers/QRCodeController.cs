using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QRCoder;
using SkiaSharp;
using System.Drawing;
using System.IO;
using TashanSofrasiWebApp.DTOs.MenuTableDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QRCodeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public QRCodeController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // API'den tüm masa bilgilerini alıyoruz
            var client = _clientFactory.CreateClient("Default");
            var responseMessage = await client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/MenuTable");
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
            string baseUrl = "https://127.0.0.1:8083/Home/Index"; // Ana sayfanın URL'si
            string fullUrl = $"{baseUrl}?id={menuTableID}";

            // QRCoder ile QR kodu verisini oluştur
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(fullUrl, QRCodeGenerator.ECCLevel.Q);

            using (var memoryStream = new MemoryStream())
            {
                // SkiaSharp ile QR kodunu çiz
                int pixelPerModule = 20; // Her bir modülün piksel boyutu
                int qrSize = qrCodeData.ModuleMatrix.Count * pixelPerModule;

                using (var surface = SKSurface.Create(new SKImageInfo(qrSize, qrSize)))
                {
                    var canvas = surface.Canvas;
                    canvas.Clear(SKColors.White);

                    var paint = new SKPaint
                    {
                        Color = SKColors.Black,
                        Style = SKPaintStyle.Fill
                    };

                    // QR kodu modüllerini SkiaSharp ile çiz
                    for (int x = 0; x < qrCodeData.ModuleMatrix.Count; x++)
                    {
                        for (int y = 0; y < qrCodeData.ModuleMatrix[x].Count; y++)
                        {
                            if (qrCodeData.ModuleMatrix[x][y]) // Modül doluysa siyah kare çiz
                            {
                                float left = x * pixelPerModule;
                                float top = y * pixelPerModule;
                                float right = left + pixelPerModule;
                                float bottom = top + pixelPerModule;

                                canvas.DrawRect(SKRect.Create(left, top, pixelPerModule, pixelPerModule), paint);
                            }
                        }
                    }

                    canvas.Flush();

                    // QR kodunu PNG formatında Base64 string'e dönüştür
                    using (var image = surface.Snapshot())
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        memoryStream.Write(data.ToArray(), 0, data.ToArray().Length);
                        ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }

            // Masa bilgilerini API'den al ve View'e gönder
            var client = _clientFactory.CreateClient("Default");
            var responseMessage = client.GetAsync($"{_configuration.GetSection("Microservices")["baseApiUrl"]}/api/MenuTable").Result;
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
