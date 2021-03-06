﻿using OnlineShop.Models;
using OnlineShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.BLL
{
    public static class clsUnit
    {
        public static IEnumerable<eUnit> GetAllWithTitle(this Repository<eUnit> repository, string msg = "[-----Tất cả-----]")
        {
            List<eUnit> lstItems = repository.GetAll();
            lstItems.Insert(0, new eUnit() { KeyID = 0, Name = msg });
            return lstItems;
        }
    }
}