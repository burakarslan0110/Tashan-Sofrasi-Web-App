using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasi.DTOLayer.FooterDTO
{
    public class UpdateFooterDTO
    {
        public int FooterID { get; set; }
        public string FooterLocation { get; set; }
        public string FooterPhoneNumber { get; set; }
        public string FooterEmail { get; set; }
        public string FooterDescription { get; set; }
        public string FooterOpeningHours { get; set; }
    }
}
