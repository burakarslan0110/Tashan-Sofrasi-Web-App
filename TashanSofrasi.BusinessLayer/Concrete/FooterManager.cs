using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DataAccessLayer.Abstract;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.BusinessLayer.Concrete
{
    public class FooterManager : IFooterService
    {
        private readonly IFooterDal _footerDal;

        public FooterManager(IFooterDal footerDal)
        {
            _footerDal = footerDal;
        }

        public void TAdd(Footer entity)
        {
            _footerDal.Add(entity); 
        }

        public void TDelete(Footer entity)
        {
            _footerDal.Delete(entity);
        }

        public Footer TGetByID(int id)
        {
           return _footerDal.GetByID(id);
        }

        public List<Footer> TGetListAll()
        {
            return _footerDal.GetListAll();
        }

        public void TUpdate(Footer entity)
        {
            _footerDal.Update(entity);  
        }
    }
}
