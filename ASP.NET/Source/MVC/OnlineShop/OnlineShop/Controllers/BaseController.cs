using OnlineShop.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    #region Custom Controller
    public interface IJsonResult
    {
        JsonResult Ok();
        JsonResult BadRequest();
        JsonResult NoContent();

        JsonResult Ok(String message);
        JsonResult BadRequest(String message);
        JsonResult NoContent(String message);

        JsonResult Ok(Object obj);
        JsonResult BadRequest(Object obj);
        JsonResult NoContent(Object obj);

        JsonResult BadRequest(Exception exception);
    }
    public class CustomController : Controller, IJsonResult
    {
        public JsonResult Ok()
        {
            return new CustomJsonResult(HttpStatusCode.OK);
        }
        public JsonResult BadRequest()
        {
            return new CustomJsonResult(HttpStatusCode.BadRequest);
        }
        public JsonResult NoContent()
        {
            return new CustomJsonResult(HttpStatusCode.NoContent);
        }

        public JsonResult Ok(String message)
        {
            return new CustomJsonResult(HttpStatusCode.OK, message);
        }
        public JsonResult BadRequest(String message)
        {
            return new CustomJsonResult(HttpStatusCode.BadRequest, message);
        }
        public JsonResult NoContent(String message)
        {
            return new CustomJsonResult(HttpStatusCode.NoContent, message);
        }

        public JsonResult Ok(Object obj)
        {
            return new CustomJsonResult(HttpStatusCode.OK, obj);
        }
        public JsonResult BadRequest(Object obj)
        {
            return new CustomJsonResult(HttpStatusCode.BadRequest, obj);
        }
        public JsonResult NoContent(Object obj)
        {
            return new CustomJsonResult(HttpStatusCode.NoContent, obj);
        }

        public JsonResult BadRequest(Exception exception)
        {
            ModelStateDictionary modelState = new ModelStateDictionary();
            modelState.AddModelError("Exception", exception);
            return new CustomJsonResult(HttpStatusCode.BadRequest, modelState);
        }
    }
    public class CustomJsonResult : JsonResult
    {
        private Int32 _statusCode;

        public CustomJsonResult(HttpStatusCode statusCode)
        {
            _statusCode = Convert.ToInt32(statusCode);
        }
        public CustomJsonResult(HttpStatusCode statusCode, String message)
        {
            _statusCode = Convert.ToInt32(statusCode);
            Data = message;

        }
        public CustomJsonResult(HttpStatusCode statusCode, Object data)
        {
            _statusCode = Convert.ToInt32(statusCode);
            Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            MaxJsonLength = Int32.MaxValue;
            ContentType = "application/json";
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            context.RequestContext.HttpContext.Response.StatusCode = _statusCode;
            base.ExecuteResult(context);
        }
    }
    #endregion

    public class BaseController<T> : CustomController where T : class, new()
    {
        #region Variable
        protected UnitOfWork Instance;
        #endregion

        #region Contructor
        public BaseController(UnitOfWork unitOfWork)
        {
            Instance = unitOfWork;
        }
        #endregion

        #region Virtual
        public virtual ActionResult Index()
        {
            return View(Instance.GetRepository<T>().GetItems(1));
        }

        public virtual ActionResult Pages(int? pageIndex)
        {
            if (!pageIndex.HasValue || pageIndex == 0 || pageIndex == 1)
                return RedirectToAction("Index");

            return View("Index", Instance.GetRepository<T>().GetItems(pageIndex.Value));
        }

        public virtual ActionResult Detail(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            T item = Instance.GetRepository<T>().FindItem(id.Value);
            if (item == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);

            return View(item);
        }

        public virtual ActionResult Create()
        {
            return View(new T());
        }

        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            T item = Instance.GetRepository<T>().FindItem(id.Value);
            if (item == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);

            return View(item);
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

        public virtual ActionResult InitItem()
        {
            return Ok(new T());
        }

        public virtual ActionResult GetItem(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            T item = Instance.GetRepository<T>().FindItem(id.Value);
            if (item == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return Ok(item);
        }

        [HttpPost]
        public virtual ActionResult CreateItem(T item)
        {
            return Ok(item);
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
        #endregion
    }
}