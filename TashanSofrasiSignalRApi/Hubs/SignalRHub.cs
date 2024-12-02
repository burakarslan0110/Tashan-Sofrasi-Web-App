using Microsoft.AspNetCore.SignalR;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DataAccessLayer.Abstract;
using TashanSofrasi.DataAccessLayer.Concrete;

namespace TashanSofrasiSignalRApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICashRegisterService _cashRegisterService;
        private readonly IMenuTableService _menuTableService;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;

        public SignalRHub(ICategoryService categoryService, IProductService productService, IOrderService orderService, ICashRegisterService cashRegisterService, IMenuTableService menuTableService, IBookingService bookingService, INotificationService notificationService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _cashRegisterService = cashRegisterService;
            _menuTableService = menuTableService;
            _bookingService = bookingService;
            _notificationService = notificationService;
        }

        public static int ClientCount { get; set; } = 0;

        public async Task SendStatistic()
        {
            var value1 = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveCategoryCount", value1);

            var value2 = _productService.TProductCount();
            await Clients.All.SendAsync("ReceiveProductCount", value2);

            var value3 = _categoryService.TActiveCategoryCount();
            await Clients.All.SendAsync("ReceiveActiveCategoryCount", value3);

            var value4 = _categoryService.TPassiveCategoryCount();
            await Clients.All.SendAsync("ReceivePassiveCategoryCount", value4);

            var value5 = _productService.TProductCountByCategoryNameCorba();
            await Clients.All.SendAsync("ReceiveProductCountByCategoryNameCorba", value5);

            var value6 = _productService.TProductCountByCategoryNamePide();
            await Clients.All.SendAsync("ReceiveProductCountByCategoryNamePide", value6);

            var value7 = _productService.TProductAveragePrice();
            await Clients.All.SendAsync("ReceiveProductAveragePrice", value7.ToString("0.00") + "₺");

            var value8 = _productService.TProductWithHighestPrice();
            await Clients.All.SendAsync("ReceiveProductWithHighestPrice", $"{value8[0]} {value8[1]}₺");

            var value9 = _productService.TProductWithLowestPrice();
            await Clients.All.SendAsync("ReceiveProductWithLowestPrice", $"{value9[0]} {value9[1]}₺");

            var value10 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", value10);

            var value11 = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", value11);

            var value12 = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveLastOrderPrice", value12.ToString("0.00") + "₺");

            var value13 = _orderService.TTodayAmount();
            await Clients.All.SendAsync("ReceiveTodayAmount", value13.ToString("0.00") + "₺");

            var value14 = _cashRegisterService.TTotalCashRegisterAmount();
            await Clients.All.SendAsync("ReceiveTotalCashRegisterAmount", value14 + "₺");

            var value15 = _productService.TProductAveragePriceByCategoryNameHamburger();
            await Clients.All.SendAsync("ReceiveProductAveragePriceByCategoryNameHamburger", value15.ToString("0.00") + "₺");

            var value16 = _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", value16);
        }

        public async Task GetBookingList()
        {
            var values = _bookingService.TGetListAll();
            await Clients.All.SendAsync("ReceiveBookingList", values);
        }

        public async Task SendNotification()
        {
            var value = _notificationService.TNotificationCountByStatusFalse();
            await Clients.All.SendAsync("ReceiveNotificationCountByStatusFalse", value);

            var notificationListByFalse = _notificationService.TGetAllNotificationByFalse();
            await Clients.All.SendAsync("ReceiveNotificationListByFalse", notificationListByFalse);
        }

        public async Task GetMenuTableStatus()
        {
            var values = _menuTableService.TGetListAll();
            await Clients.All.SendAsync("ReceiveMenuTableStatus", values);
        }

        public override async Task OnConnectedAsync()
        {
            ClientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ClientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
