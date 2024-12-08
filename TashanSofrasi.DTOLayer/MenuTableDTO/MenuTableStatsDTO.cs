using TashanSofrasi.DTOLayer.MenuTableDTO;
using TashanSofrasi.DTOLayer.OrderDetailDTOs;
using TashanSofrasi.DTOLayer.ProductDTO;

namespace TashanSofrasi.DTOLayer.MenuTableDTO
{
    public class MenuTableStatsDTO
    {
        public UpdateMenuTableDTO MenuTableDTO { get; set; }
        public List<OrderDetailDTO> orderDetailDTO { get; set; }
    }
}
