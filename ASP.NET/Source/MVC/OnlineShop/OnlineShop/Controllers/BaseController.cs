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

        public virtual ActionResult GetAll()
        {
            return View(Instance.GetRepository<T>().GetAll());
        }

        public virtual ActionResult FindByID(int? id)
        {
            if (id.HasValue)
                return View(Instance.GetRepository<T>().FindItem(id) ?? new T());
            return View(new T());
        }

        [HttpPost]
        public virtual ActionResult InsertItem(T item)
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult InsertItems(T[] items)
        {
            return View();
        }

        [HttpPut]
        public virtual ActionResult UpdateItem(T item)
        {
            return View();
        }

        [HttpPut]
        public virtual ActionResult UpdateItems(T[] items)
        {
            return View();
        }

        [HttpDelete]
        public virtual ActionResult DeleteItem(T item)
        {
            return View();
        }

        [HttpDelete]
        public virtual ActionResult DeleteItems(T[] items)
        {
            return View();
        }
    }
}