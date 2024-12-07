using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasi.DTOLayer.DiscountDTO
{
    public class GetDiscountDTO
    {
        public int DiscountID { get; set; }
        public string DiscountTitle { get; set; }
        public int DiscountAmount { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountImageURL { get; set; }

		public bool DiscountStatus { get; set; }
	}
}
