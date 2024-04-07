using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;
using WebBanLaptop.ViewModel;

namespace WebBanLaptop.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            ViewBag.CurrentController = "Profile";
            if (Session["user"] == null)
                return RedirectToAction("Index", "Login");
            using (var ctx = new DBContext())
            {
                var user = Session["user"] as user;
                var listOrder = ctx.orders
                    .Where(item => item.user_id == user.user_id)
                    .ToList()
                    .Select(item => new order
                    {
                        order_id = item.order_id,
                        created_at = item.created_at,
                        total_price = item.total_price,
                        total_quantity = item.total_quantity,
                        full_name = item.full_name,
                        shipping_address = item.shipping_address,
                        phone_number = item.phone_number,
                        order_items = item.order_items
                        .Select(oitem => new order_items
                        {
                            product_id = oitem.product_id,
                            product = oitem.product,
                            total_price = oitem.total_price,
                            quantity = oitem.quantity,
                        })
                        .ToList()
                    })
                    .ToList();
                var profileModel = new ProfileViewModel(listOrder);
                return View(profileModel);
            }
        }
    }
}