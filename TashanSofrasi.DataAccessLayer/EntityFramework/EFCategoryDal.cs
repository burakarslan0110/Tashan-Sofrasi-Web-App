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
    public class EFCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public EFCategoryDal(TashanSofrasiContext context) : base(context)
        {
        }

        public int CategoryCount()
        {
            using var context = new TashanSofrasiContext();
            return context.Categories.Count();
        }

        public int ActiveCategoryCount()
        {
            using (var context = new TashanSofrasiContext())
            {
                return context.Categories.Where(x => x.CategoryStatus == true).Count();
            }
        }

        public int PassiveCategoryCount()
        {
            using (var context = new TashanSofrasiContext())
            {
                return context.Categories.Where(x => x.CategoryStatus == false).Count();
            }
        }

        public void CategoryStatusChangeToFalse(int id)
        {
            using (var context = new TashanSofrasiContext())
            {
                var values = context.Categories.Find(id);
                values.CategoryStatus = false;
                context.SaveChanges();
            }
        }

        public void CategoryStatusChangeToTrue(int id)
        {
            using (var context = new TashanSofrasiContext())
            {
                var values = context.Categories.Find(id);
                values.CategoryStatus = true;
                context.SaveChanges();  
            }
        }
    }
}
