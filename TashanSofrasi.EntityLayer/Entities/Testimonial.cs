using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TashanSofrasi.EntityLayer.Entities
{
    public class Testimonial
    {
        public int TestimonialID { get; set; }
        public string TestimonialNameSurname { get; set; }  
        public string TestimonialTitle { get; set; }  
        public string TestimonialComment { get; set; }  
        public string TestimonialImageURL { get; set; }  
        public bool TestimonialStatus { get; set; }  
    }
}
