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
    public class EFTestimonialDal : GenericRepository<Testimonial>, ITestimonialDal
    {
        public EFTestimonialDal(TashanSofrasiContext context) : base(context)
        {
        }

        public void ChangeTestimonialStatusToFalse(int id)
        {
            using (TashanSofrasiContext context = new TashanSofrasiContext())
            {
                var values = context.Testimonials.Find(id);
                values.TestimonialStatus = false;
                context.SaveChanges();
            }
        }

        public void ChangeTestimonialStatusToTrue(int id)
        {
            using (TashanSofrasiContext context = new TashanSofrasiContext())
            {
                var values = context.Testimonials.Find(id);
                values.TestimonialStatus = true;
                context.SaveChanges();
            }
        }
    }
}
