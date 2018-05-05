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

        public ActionResult SignIn(string username, string password)
        {
            LoginRequest login = new LoginRequest();
            login.Username = username;
            login.Password = password;

            return View(login);
        }

        [HttpPost]
        public ActionResult SignIn(LoginRequest login)
        {
            bool IsValid = true;

            if (login == null)
            {
                ModelState.AddModelError("", "Đăng nhập không thành công.");
                IsValid = false;
            }
            if (login.Username.IsEmpty())
            {
                ModelState.AddModelError(nameof(login.Username), "Vui lòng nhập tài khoản.");
                IsValid = false;
            }
            if (login.Password.IsEmpty())
            {
                ModelState.AddModelError(nameof(login.Password), "Vui lòng nhập mật khẩu.");
                IsValid = false;
            }

            if (IsValid)
            {
                clsEnum.fLogin res = Instance.CheckLogin(login.Username, login.Password);
                switch (res)
                {
                    case clsEnum.fLogin.NotFound:
                        ModelState.AddModelError("", "Tài khoản không tồn tại.");
                        goto default;
                    case clsEnum.fLogin.Disable:
                        ModelState.AddModelError("", "Tài khoản đã bị khóa.");
                        goto default;
                    case clsEnum.fLogin.Success:
                        return RedirectToAction("Index", "Home");
                    default:
                        return View("Index");
                }
            }
            return View("Index");
        }
    }
}