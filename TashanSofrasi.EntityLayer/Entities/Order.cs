using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasi.EntityLayer.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int MenuTableID { get; set; } 
        public MenuTable MenuTable { get; set; }
        public bool OrderStatus { get; set; }

        [Column(TypeName ="Date")]
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
