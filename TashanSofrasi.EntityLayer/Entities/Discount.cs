namespace TashanSofrasi.EntityLayer.Entities
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public string DiscountTitle { get; set; }
        public int DiscountAmount { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountImageURL { get; set; }
        public bool DiscountStatus { get; set; }
	}
}
