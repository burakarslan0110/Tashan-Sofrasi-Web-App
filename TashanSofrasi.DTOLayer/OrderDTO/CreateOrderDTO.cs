using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasi.DTOLayer.OrderDTO
{
    public class CreateOrderDTO
    {
        public int MenuTableID { get; set; }
        public bool OrderStatus { get; set; }
        public string OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
