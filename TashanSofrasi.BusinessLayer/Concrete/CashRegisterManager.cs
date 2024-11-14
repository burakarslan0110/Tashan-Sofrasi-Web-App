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
    public class CashRegisterManager : ICashRegisterService
    {
        private readonly ICashRegisterDal _cashRegisterDal;

        public CashRegisterManager(ICashRegisterDal cashRegisterDal)
        {
            _cashRegisterDal = cashRegisterDal;
        }

        public void TAdd(CashRegister entity)
        {
            _cashRegisterDal.Add(entity);
        }

        public void TDelete(CashRegister entity)
        {
            _cashRegisterDal.Delete(entity);
        }

        public CashRegister TGetByID(int id)
        {
           return _cashRegisterDal.GetByID(id);
        }

        public List<CashRegister> TGetListAll()
        {
            return _cashRegisterDal.GetListAll();
        }

        public decimal TTotalCashRegisterAmount()
        {
           return _cashRegisterDal.TotalCashRegisterAmount();
        }

        public void TUpdate(CashRegister entity)
        {
            _cashRegisterDal.Update(entity);    
        }
    }
}
