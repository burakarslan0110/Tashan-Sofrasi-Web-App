﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.BusinessLayer.Abstract
{
    public interface ITestimonialService : IGenericService<Testimonial>
    {
        void TChangeTestimonialStatusToFalse(int id);
        void ChangeTestimonialStatusToTrue(int id);
    }
}
