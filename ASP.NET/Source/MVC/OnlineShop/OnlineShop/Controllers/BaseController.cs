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

        public virtual ActionResult Index()
        {
            return View(Instance.GetRepository<T>().GetAll());
        }

        public virtual ActionResult Detail(int? id)
        {
            if (id.HasValue)
                return View(Instance.GetRepository<T>().FindItem(id) ?? new T());
            return View(new T());
        }

        public virtual ActionResult Create()
        {
            return View();
        }

        public virtual ActionResult Edit()
        {
            return View();
        }

        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            if (Instance.GetRepository<T>().FindItem(id.Value) == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);

            return View();
            //return new HttpStatusCodeResult(System.Net.HttpStatusCode.Found);
        }

        [HttpPost]
        public virtual ActionResult CreateItem(T item)
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult CreateItems(T[] items)
        {
            return View();
        }

        [HttpPut]
        public virtual ActionResult EditItem(T item)
        {
            return View();
        }

        [HttpPut]
        public virtual ActionResult EditItems(T[] items)
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