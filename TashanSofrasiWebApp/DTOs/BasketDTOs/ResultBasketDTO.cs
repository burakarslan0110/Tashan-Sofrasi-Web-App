using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiWebApp.DTOs.BasketDTOs
{
    public class ResultBasketDTO
    {
        public int BasketID { get; set; }

        public int ProductID { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public decimal TotalPrice { get; set; }

        public int MenuTableID { get; set; }

        public Product Product { get; set; }

    }
}
