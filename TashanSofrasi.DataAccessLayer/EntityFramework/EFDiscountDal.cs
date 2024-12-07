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
    public class EFDiscountDal : GenericRepository<Discount>, IDiscountDal
    {
        public EFDiscountDal(TashanSofrasiContext context) : base(context)
        {
        }

		public void ChangeDiscountStatusToFalse(int id)
		{
			using (var context = new TashanSofrasiContext())
			{
				var discount = context.Discounts.Find(id);
				discount.DiscountStatus = false;
				context.SaveChanges();
			}
		}

		public void ChangeDiscountStatusToTrue(int id)
		{
			using (var context = new TashanSofrasiContext())
			{
				var discount = context.Discounts.Find(id);
				discount.DiscountStatus = true;
				context.SaveChanges();
			}
		}
	}
}
