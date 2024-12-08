﻿using TashanSofrasiWebApp.DTOs.CategoryDTOs;

namespace TashanSofrasiWebApp.DTOs.ProductDTOs
{
	public class ResultProductDTO
	{
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public decimal ProductPrice { get; set; }
		public string ProductImageURL { get; set; }
		public bool ProductStatus { get; set; }
		public ResultCategoryDTO Category { get; set; }
		public IFormFile ProductImage { get; set; }

	}
}
