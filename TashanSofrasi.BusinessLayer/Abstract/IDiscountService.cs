﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.BusinessLayer.Abstract
{
    public interface IDiscountService : IGenericService<Discount>
    {
		void TChangeDiscountStatusToFalse(int id);
		void TChangeDiscountStatusToTrue(int id);
	}
}
