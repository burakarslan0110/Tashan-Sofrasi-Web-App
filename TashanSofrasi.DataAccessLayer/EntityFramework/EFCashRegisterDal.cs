using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.DataAccessLayer.Abstract;
using TashanSofrasi.DataAccessLayer.Concrete;
using TashanSofrasi.DataAccessLayer.Repositories;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.DataAccessLayer.EntityFramework
{
    public class EFCashRegisterDal : GenericRepository<CashRegister>, ICashRegisterDal
    {
        public EFCashRegisterDal(TashanSofrasiContext context) : base(context)
        {
        }

        public decimal TotalCashRegisterAmount()
        {
            using (var context = new TashanSofrasiContext())
            {
                return context.CashRegisters.Select(x=>x.TotalAmount).FirstOrDefault();
            }
        }
    }
}
