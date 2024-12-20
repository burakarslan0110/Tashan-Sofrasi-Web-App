﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.DataAccessLayer.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
       int CategoryCount();
       int ActiveCategoryCount();
       int PassiveCategoryCount();
       void CategoryStatusChangeToFalse(int id);
       void CategoryStatusChangeToTrue(int id);
    }
}
