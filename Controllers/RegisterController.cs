using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult doRegister(user user, FormCollection form)
        {
            using(var ctx = new DBContext())
            {
                if (!user.password.Equals(form["confirm-password"]))
                {
                    Session["error"] = "Nhập lại mật khẩu không khớp";
                    return View("Index");
                }
                var isExistEmail = ctx.users.FirstOrDefault(item => item.email.Equals(user.email));
                if (isExistEmail != null)
                {
                    Session["error"] = "Email đã tồn tại";
                    return View("Index");
                }
                user.role = "user";
                user.created_at = DateTime.Now;
                user.updated_at = DateTime.Now;
                user.status = "active";
                ctx.users.Add(user);
                ctx.SaveChanges();
            }
            Session["success"] = "Đăng ký thành công";
            return RedirectToAction("Index", "Login");   
        }
    }
}