using TashanSofrasi.DTOLayer.ProductDTO;

namespace TashanSofrasi.DTOLayer.OrderDetailDTOs
{
    public class OrderDetailDTO
    {
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public int MenuTableID { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public GetProductDTO Product { get; set; }
    }
}
