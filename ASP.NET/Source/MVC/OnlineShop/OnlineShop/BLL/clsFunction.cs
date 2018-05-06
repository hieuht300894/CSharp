using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop
{
    public static class clsFunction
    {
        public static IEnumerable<T> GetItems<T>(this Repository<T> repository, int page) where T : class, new()
        {
            return repository.GetAll().ToPagedList(page, clsGeneral.pageSize);
        }
    }
}