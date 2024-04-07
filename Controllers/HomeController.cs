using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;
using WebBanLaptop.ViewModel;

namespace WebBanLaptop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CurrentController = "Home";
            using (var ctx = new DBContext())
            {
                var listCategory = ctx.categories
                    .Where(item => item.status.Equals("active"))
                    .ToList()
                    .Select(item => new category
                    {
                        category_id = item.category_id,
                        category_name = item.category_name,
                        products = item.products
                    })
                    .ToList();
                var model = new HomeViewModel(listCategory);
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index");
        }
    }
}