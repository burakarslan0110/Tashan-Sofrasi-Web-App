﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasiWebApp.DTOs.ContactDTOs
{
    public class CreateContactDTO
    {
        public string ContactNameSurname { get; set; }
        public string ContactEMail { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactMessage { get; set; }
        public string ContactDate { get; set; }
    }
}
