using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasi.EntityLayer.Entities
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string NotificationDescription { get; set; }
        public string NotificationType { get; set; }
        public string NotificationIcon { get; set; }

        [Column(TypeName = "Date")]
        public DateTime NotificationDate { get; set; }
        public bool NotificationStatus { get; set; }
    }
}
