using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class LoginController : BaseController<xAccount>
    {
        public LoginController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckLogin(LoginRequest login)
        {
            bool IsSuccess = true;

            if (login == null)
            {
                ModelState.AddModelError("", "Đăng nhập không thành công.");
                IsSuccess = false;
            }
            if (login.Username.IsEmpty())
            {
                ModelState.AddModelError(nameof(login.Username), "Vui lòng nhập username.");
                IsSuccess = false;
            }
            if (login.Password.IsEmpty())
            {
                ModelState.AddModelError(nameof(login.Password), "Vui lòng nhập password.");
                IsSuccess = false;
            }

            if (IsSuccess)
                return RedirectToAction("Index", "Home");
            return View("Index");
        }
    }
}