using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;
using WebBanLaptop.ViewModel;

namespace WebBanLaptop.Areas.Admin.Controllers
{
    public class ProductAdminController : Controller
    {
        // GET: Admin/ProductAdmin
        public ActionResult Index()
        {
            using (var ctx = new DBContext())
            {
                var list = ctx.products
                    .Where(item => item.status.Equals("active"))
                    .ToList()
                    .Select(item => new product
                    {
                        product_id = item.product_id,
                        product_name = item.product_name,
                        product_image = item.product_image,
                        status = item.status,
                        price = item.price,
                        category_id = item.category_id,
                        description = item.description,
                        category = new category
                        {
                            category_name = item.category.category_name
                        }
                    })
                    .ToList();
                return View(list);
            }
        }

        public ActionResult Add()
        {
            using (var ctx = new DBContext())
            {
                var listCate = ctx.categories.Where(item => item.status.Equals("active")).ToList();
                return View(new ProductViewModel(listCate, new product()));
            }
        }

        [HttpPost]
        public ActionResult doAdd(product product, HttpPostedFileBase img, FormCollection form)
        {
            using (var ctx = new DBContext())
            {
                var p = new product();
                p.price = product.price;
                p.product_name = product.product_name;
                p.description = product.description;
                p.status = "active";
                p.created_at = DateTime.Now;
                p.updated_at = DateTime.Now;
                if (img.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(img.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/images"), _FileName);
                    img.SaveAs(_path);
                    p.product_image = "~/Content/images/" + _FileName;
                }
                var cateId = form["cate"];
                p.category_id = Convert.ToInt32(cateId);
                ctx.products.Add(p);
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "ProductAdmin");
        }


        public ActionResult Edit(int id)
        {
            using (var ctx = new DBContext())
            {
                var product = ctx.products.FirstOrDefault(item => item.product_id == id);
                if (product == null)
                {
                    return RedirectToAction("Index", "ProductAdmin");
                }
                var listCate = ctx.categories.Where(item => item.status.Equals("active")).ToList();
                var pModel = new ProductViewModel(listCate, product);
                return View(pModel);
            }
        }

        [HttpPost]
        public ActionResult doUpdate(product product, HttpPostedFileBase img, FormCollection form)
        {
            using (var ctx = new DBContext())
            {
                var p = ctx.products.FirstOrDefault(item => item.product_id == product.product_id);
                p.price = product.price;
                p.product_name = product.product_name;
                p.description = product.description;
                p.status = "active";
                p.updated_at = DateTime.Now;
                if (img.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(img.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/images"), _FileName);
                    img.SaveAs(_path);
                    p.product_image = "~/Content/images/" + _FileName;
                }
                var cateId = form["cate"];
                p.category_id = Convert.ToInt32(cateId);
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "ProductAdmin");
        }


        public ActionResult delete(int id)
        {
            using (var ctx = new DBContext())
            {
                var p = ctx.products.FirstOrDefault(item => item.product_id == id);
                if (p != null)
                {
                    p.status = "inactive";
                    p.updated_at = DateTime.Now;
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index", "ProductAdmin");
        }
    }
}