using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasiWebApp.DTOs.MenuTableDTOs
{
    public class UpdateMenuTableDTO
    {
        public int MenuTableID { get; set; }
        public string MenuTableName { get; set; }
        public bool MenuTableStatus { get; set; }
    }
}
