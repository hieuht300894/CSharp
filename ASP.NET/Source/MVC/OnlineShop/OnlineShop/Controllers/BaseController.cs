using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop
{
    public class BaseController<T> : Controller where T : class, new()
    {
        protected UnitOfWork Instance;

        public BaseController(UnitOfWork unitOfWork)
        {
            Instance = unitOfWork;
        }
    }
}