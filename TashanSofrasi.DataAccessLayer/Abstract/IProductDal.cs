using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.DataAccessLayer.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        List<Product> GetProductWithCategories();
        int ProductCount();
        int ProductCountByCategoryNamePide();
        int ProductCountByCategoryNameCorba();
        decimal ProductAveragePrice();
        decimal GetProductPriceByProductID(int id);
        decimal ProductAveragePriceByCategoryNameHamburger();
        List<string> ProductWithHighestPrice();
        List<string> ProductWithLowestPrice();


    }
}
