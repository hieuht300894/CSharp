using OnlineShop.BLL;
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
            ViewBag.Units = Instance.GetRepository<eUnit>().GetAllWithTitle();
            return base.Index();
        }

        public override ActionResult Detail(int? id)
        {
            return base.Detail(id);
        }

        public override ActionResult Create()
        {
            ViewBag.Units = Instance.GetRepository<eUnit>().GetAll();
            return base.Create();
        }

        public override ActionResult Edit(int? id)
        {
            return base.Edit(id);
        }

        public override ActionResult Delete(int? id)
        {
            return base.Delete(id);
        }

        public override ActionResult Pages(int? pageIndex)
        {
            return base.Pages(pageIndex);
        }
    }
}