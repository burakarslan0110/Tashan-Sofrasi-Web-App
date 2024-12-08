namespace TashanSofrasiWebApp.DTOs.OrderDTOs
{
    public class UpdateOrderDTO
    {
        public int OrderID { get; set; }
        public int MenuTableID { get; set; }
        public bool OrderStatus { get; set; }
        public string OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
