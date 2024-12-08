using TashanSofrasi.DTOLayer.MenuTableDTO;
using TashanSofrasi.DTOLayer.ProductDTO;
using TashanSofrasiWebApp.DTOs.OrderDetailDTOs;
using TashanSofrasiWebApp.DTOs.OrderDTOs;

namespace TashanSofrasiWebApp.Areas.Admin.Models
{
    public class MenuTableStatsViewModel
    {
        public UpdateMenuTableDTO MenuTableDTO { get; set; }
        public OrderDetailDTO OrderDetailDTO { get; set; }
    }
}
