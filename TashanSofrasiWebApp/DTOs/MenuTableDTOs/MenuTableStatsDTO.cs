
using TashanSofrasiWebApp.DTOs.OrderDetailDTOs;

namespace TashanSofrasiWebApp.DTOs.MenuTableDTOs
{
    public class MenuTableStatsDTO
    {
        public UpdateMenuTableDTO? updateMenuTableDTO { get; set; }
        public List<OrderDetailDTO>? orderDetailDTO { get; set; }
    }
}
