﻿namespace TashanSofrasi.EntityLayer.Entities
{
    public class Booking
    {
        public int BookingID { get; set; }
        public string BookingNameSurname { get; set; }
        public string BookingPhoneNumber { get; set; }
        public string BookingEmail { get; set; }
        public int BookingPersonCount { get; set; }
        public DateTime BookingDate { get; set; }
    }
}