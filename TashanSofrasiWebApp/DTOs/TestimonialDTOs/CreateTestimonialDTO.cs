﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasiWebApp.DTOs.TestimonialDTOs
{
    public class CreateTestimonialDTO
    {
        public string TestimonialNameSurname { get; set; }
        public string TestimonialTitle { get; set; }
        public string TestimonialComment { get; set; }
        public string TestimonialImageURL { get; set; }
        public bool TestimonialStatus { get; set; }
        public IFormFile TestimonialImage { get; set; }
}
}
