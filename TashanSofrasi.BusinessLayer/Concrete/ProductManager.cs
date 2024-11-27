using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DataAccessLayer.Abstract;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void TAdd(Product entity)
        {
            _productDal.Add(entity);    
        }

        public void TDelete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public Product TGetByID(int id)
        {
           return _productDal.GetByID(id);
        }

        public List<Product> TGetListAll()
        {
           return _productDal.GetListAll();
        }

        public List<Product> TGetProductWithCategories()
        {
            return _productDal.GetProductWithCategories();
        }

        public void TUpdate(Product entity)
        {
            _productDal.Update(entity);
        }

        public int TProductCount()
        {
            return _productDal.ProductCount();
        }

        public int TProductCountByCategoryNamePide()
        {
            return _productDal.ProductCountByCategoryNamePide();
        }

        public int TProductCountByCategoryNameCorba()
        {
            return _productDal.ProductCountByCategoryNameCorba();
        }

        public decimal TProductAveragePrice()
        {
            return _productDal.ProductAveragePrice();
        }

        public List<string> TProductWithHighestPrice()
        {
            return _productDal.ProductWithHighestPrice();
        }

        public List<string> TProductWithLowestPrice()
        {
            return _productDal.ProductWithLowestPrice();
        }

        public decimal TProductAveragePriceByCategoryNameHamburger()
        {
           return _productDal.ProductAveragePriceByCategoryNameHamburger();
        }

        public decimal TGetProductPriceByProductID(int id)
        {
            return _productDal.GetProductPriceByProductID(id);
        }
    }
}
