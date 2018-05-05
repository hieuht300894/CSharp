using OnlineShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop
{
    public static class clsProduct
    {
        public static IEnumerable<eProduct> GetProducts(this Repository<eProduct> repository, int page)
        {
            return repository.GetAll().ToPagedList(page, clsGeneral.pageSize);
        }
    }
}