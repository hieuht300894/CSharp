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

        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Detail(int? id)
        {
            return base.Detail(id);
        }

        public override ActionResult Create()
        {
            return base.Create();
        }

        public override ActionResult Edit()
        {
            return base.Edit();
        }

        public override ActionResult Delete(int? id)
        {
            return base.Delete(id);
        }
    }
}