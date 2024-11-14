using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasi.DTOLayer.BookingDTO
{
    public class UpdateBookingDTO
    {
        public int BookingID { get; set; }
        public string BookingNameSurname { get; set; }
        public string BookingPhoneNumber { get; set; }
        public string BookingEmail { get; set; }
        public int BookingPersonCount { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
