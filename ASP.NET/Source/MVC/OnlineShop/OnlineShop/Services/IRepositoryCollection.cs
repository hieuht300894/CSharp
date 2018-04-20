using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop
{
    public interface IRepositoryCollection
    {
        Repository<T> GetRepository<T>() where T : class;
    }
}