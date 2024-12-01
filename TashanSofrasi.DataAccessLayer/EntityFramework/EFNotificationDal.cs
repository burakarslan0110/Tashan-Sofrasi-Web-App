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
    public class EFNotificationDal : GenericRepository<Notification>, INotificationDal
    {
        public EFNotificationDal(TashanSofrasiContext context) : base(context)
        {
        }

        public List<Notification> GetAllNotificationByFalse()
        {
            using (var context = new TashanSofrasiContext())
            {
                return context.Notifications.Where(x => x.NotificationStatus == false).ToList();
            }
        }

        public int NotificationCountByStatusFalse()
        {
            using (var context = new TashanSofrasiContext())
            {
                return context.Notifications.Where(x => x.NotificationStatus == false).Count();
            }
        }

        public void NotificationRead()
        {
            using (var context = new TashanSofrasiContext())
            {
                var values = context.Notifications.Where(x => x.NotificationStatus == false);
                foreach (var item in values)
                {
                    item.NotificationStatus = true;
                }
                context.SaveChanges();
            }
        }
    }
}
