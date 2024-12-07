﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.DataAccessLayer.Abstract
{
    public interface IDiscountDal : IGenericDal<Discount>
    {
        void ChangeDiscountStatusToTrue(int id);
		void ChangeDiscountStatusToFalse(int id);
	}
}
