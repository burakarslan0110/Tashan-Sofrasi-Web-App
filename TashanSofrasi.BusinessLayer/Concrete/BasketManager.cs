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
    public class BasketManager : IBasketService
    {
        private readonly IBasketDal _basketDal;

        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }

        public void TAdd(Basket entity)
        {
            _basketDal.Add(entity);
        }

        public void TDelete(Basket entity)
        {
            throw new NotImplementedException();
        }

        public List<Basket> TGetBasketByMenuTableID(int id)
        {
            return _basketDal.GetBasketByMenuTableID(id);    
        }

        public Basket TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Basket> TGetListAll()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Basket entity)
        {
            throw new NotImplementedException();
        }
    }
}
