﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasi.DTOLayer.ContactDTO
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