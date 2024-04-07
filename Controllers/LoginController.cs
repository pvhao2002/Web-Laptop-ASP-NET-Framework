using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult doLogin(user user)
        {
            using (var ctx = new DBContext())
            {
                var u = ctx.users.FirstOrDefault(item => item.email.Equals(user.email));
                if (u != null && u.password.Equals(user.password))
                {
                    Session["user"] = u;
                    if (u.role.Equals("admin"))
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            Session["error"] = "Email hoặc mật khẩu không đúng";
            return View("Index");
        }
    }
}