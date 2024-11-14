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
        decimal TProductAveragePriceByCategoryNameHamburger();

        List<string> TProductWithHighestPrice();

        List<string> TProductWithLowestPrice();
    }
}
