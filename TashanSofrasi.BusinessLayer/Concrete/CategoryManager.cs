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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        

        public void TAdd(Category entity)
        {
            _categoryDal.Add(entity);
        }

        public int TCategoryCount()
        {
            return _categoryDal.CategoryCount();
        }

        public void TDelete(Category entity)
        {
            _categoryDal.Delete(entity);
        }

        public Category TGetByID(int id)
        {
            return _categoryDal.GetByID(id);    
        }

        public List<Category> TGetListAll()
        {
            return _categoryDal.GetListAll();
        }
        
        public void TUpdate(Category entity)
        {
            _categoryDal.Update(entity);
        }
        public int TPassiveCategoryCount()
        {
            return _categoryDal.PassiveCategoryCount();
        }
        public int TActiveCategoryCount()
        {
            return _categoryDal.ActiveCategoryCount();
        }
    }
}
