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
            ViewBag.Title = "List all of products";
            return base.Index();
        }

        public override ActionResult Detail(int? id)
        {
            ViewBag.Title = "Product detail";
            return base.Detail(id);
        }

        public override ActionResult Create()
        {
            ViewBag.Title = "Create product";
            return base.Create();
        }

        public override ActionResult Edit(int? id)
        {
            ViewBag.Title = "Edit product";
            return base.Edit(id);
        }

        public override ActionResult Delete(int? id)
        {
            ViewBag.Title = "Delete product";
            return base.Delete(id);
        }
    }
}