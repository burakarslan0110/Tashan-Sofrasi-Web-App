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
    public class EFMenuTableDal : GenericRepository<MenuTable>, IMenuTableDal
    {
        public EFMenuTableDal(TashanSofrasiContext context) : base(context)
        {
        }

        public int MenuTableCount()
        {
            using (var context = new TashanSofrasiContext())
            {
                return context.MenuTables.Where(x => x.MenuTableStatus != false).Count();
            }
        }
    }
}
