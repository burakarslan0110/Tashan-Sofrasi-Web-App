namespace TashanSofrasiWebApp.DTOs.BookingDTOs
{
    public class CreateBookingDTO
    {
        public string BookingNameSurname { get; set; }
        public string BookingPhoneNumber { get; set; }
        public string BookingEmail { get; set; }
        public int BookingPersonCount { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
