namespace TashanSofrasiWebApp.DTOs.BasketDTOs
{
    public class CreateBasketDTO
    {
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductID { get; set; }
        public int MenuTableID { get; set; }
    }
}
