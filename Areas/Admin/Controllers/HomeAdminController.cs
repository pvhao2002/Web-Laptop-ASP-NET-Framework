using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;
using WebBanLaptop.ViewModel;

namespace WebBanLaptop.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            using (var ctx = new DBContext())
            {
                var totalP = ctx.products.Where(item => item.status.Equals("active")).ToList().Count;
                var totalO = ctx.orders.ToList().Count;
                var totalR = ctx.orders.Sum(item => item.total_price).Value;
                var homeModel = new HomeAdminViewModel(totalP, totalO, totalR);
                return View(homeModel);
            }
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}