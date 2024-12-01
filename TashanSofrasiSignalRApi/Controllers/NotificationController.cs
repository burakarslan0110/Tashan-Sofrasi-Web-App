using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.BusinessLayer.Abstract;

namespace TashanSofrasiSignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult INotificationList()
        {
            var value = _notificationService.TGetListAll();
            return Ok(value);
        }

        [HttpGet("NotificationCountByStatusFalse")]
        public IActionResult NotificationCountByStatusFalse()
        {
            var value = _notificationService.TNotificationCountByStatusFalse();
            return Ok(value);
        }

        [HttpGet("GetAllNotificationByFalse")]
        public IActionResult GetAllNotificationByFalse()
        {
            var value = _notificationService.TGetAllNotificationByFalse();
            return Ok(value);
        }

        [HttpGet("NotificationRead")]
        public IActionResult NotificationRead()
        {
            _notificationService.TNotificationRead();
            return Ok("Bildirimler okundu olarak işaretlendi!");
        }
    }
}
