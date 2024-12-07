using Microsoft.EntityFrameworkCore;
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
    public class EFBasketDal : GenericRepository<Basket>, IBasketDal
    {
        public EFBasketDal(TashanSofrasiContext context) : base(context)
        {
        }

        public async void ClearBasket(List<Basket> basket)
        {
            using (var context = new TashanSofrasiContext())
            {
                context.Baskets.RemoveRange(basket);
                await context.SaveChangesAsync();
            }
        }

        public List<Basket> GetBasketByMenuTableID(int id)
        {
            using (var context = new TashanSofrasiContext())
            {
                var values = context.Baskets.Where(x => x.MenuTableID == id).Include(y => y.Product).ToList();
                return values;
            }

        }

        public Basket GetBasketByProductID(int productID, int menutableid)
        {
            using (var context = new TashanSofrasiContext())
            {
                var values = context.Baskets.Where(x=>x.ProductID == productID).Where(y => y.MenuTableID == menutableid).Include(z => z.Product).FirstOrDefault();  
                return values;
            }
        }
    }
}
