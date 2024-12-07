using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasiWebApp.DTOs.DiscountDTOs
{
    public class UpdateDiscountDTO
    {
        public int DiscountID { get; set; }
        public string DiscountTitle { get; set; }
        public int DiscountAmount { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountImageURL { get; set; }
        public IFormFile DiscountImage { get; set; }
		public bool DiscountStatus { get; set; }
	}
}
