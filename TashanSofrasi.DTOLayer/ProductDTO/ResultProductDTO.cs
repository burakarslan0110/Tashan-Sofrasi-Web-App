using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.DTOLayer.CategoryDTO;

namespace TashanSofrasi.DTOLayer.ProductDTO
{
    public class ResultProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageURL { get; set; }
        public bool ProductStatus { get; set; }
		public int CategoryID { get; set; }
		public ResultCategoryDTO Category { get; set; }
	}
}
