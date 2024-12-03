using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.BusinessLayer.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        List<Product> TGetProductWithCategories();
        int TProductCount();
        int TProductCountByCategoryNamePide();
        int TProductCountByCategoryNameCorba();
        decimal TProductAveragePrice();
        decimal TGetProductPriceByProductID(int id);
        decimal TProductAveragePriceByCategoryNameHamburger();
        List<string> TProductWithHighestPrice();
        List<string> TProductWithLowestPrice();
        void TChangeProductStatusToTrue(int id);
        void TChangeProductStatusToFalse(int id);
    }
}
