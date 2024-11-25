﻿using System;
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
    public class EFSocialMediaDal : GenericRepository<SocialMedia>, ISocialMediaDal
    {
        public EFSocialMediaDal(TashanSofrasiContext context) : base(context)
        {
        }
    }
}