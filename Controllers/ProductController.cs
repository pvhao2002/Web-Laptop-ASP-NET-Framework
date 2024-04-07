using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;
using WebBanLaptop.ViewModel;

namespace WebBanLaptop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int? cate)
        {
            ViewBag.CurrentController = "Product";
            using (var ctx = new DBContext())
            {
                var listCate = ctx.categories.Where(item => item.status.Equals("active")).ToList();
                List<product> products;
                if (cate != null)
                {
                    products = ctx.products.Where(item => item.category_id == cate && item.status.Equals("active")).ToList();
                }
                else
                {
                    products = ctx.products.Where(item => "active".Equals(item.status)).ToList();
                }
                var productModel = new ProductViewModel(products, listCate);
                return View(productModel);
            }
        }

        public ActionResult Detail(int id)
        {
            ViewBag.CurrentController = "Product";
            using (var ctx = new DBContext())
            {
                var p = ctx.products.FirstOrDefault(item => item.product_id == id);
                if(p == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(p);
            }
        }
    }
}