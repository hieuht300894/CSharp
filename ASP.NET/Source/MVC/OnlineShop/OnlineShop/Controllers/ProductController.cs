using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : BaseController<eProduct>
    {
        public ProductController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ActionResult GetAll()
        {
            return View();
        }
    }
}