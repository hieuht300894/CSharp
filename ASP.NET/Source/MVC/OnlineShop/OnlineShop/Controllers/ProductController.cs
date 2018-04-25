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

        public override ActionResult GetAll()
        {
            return base.GetAll();
        }

        public override ActionResult FindByID(int? id)
        {
            return base.FindByID(id);
        }

        public override ActionResult InsertItem(eProduct item)
        {
            return base.InsertItem(item);
        }

        public override ActionResult UpdateItem(eProduct item)
        {
            return base.UpdateItem(item);
        }

        public override ActionResult DeleteItem(eProduct item)
        {
            return base.DeleteItem(item);
        }
    }
}