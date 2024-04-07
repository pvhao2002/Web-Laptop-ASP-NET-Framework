using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanLaptop.Models;

namespace WebBanLaptop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            ViewBag.CurrentController = "Cart";
            if (Session["user"] != null)
            {
                using (var ctx = new DBContext())
                {
                    var user = Session["user"] as user;
                    var cart = ctx.carts
                        .FirstOrDefault(item => item.user_id == user.user_id);
                    var cartTemp = new cart
                    {
                        cart_id = cart.cart_id,
                        total_price = cart.total_price,
                        total_quantity = cart.total_quantity,
                        cart_items = cart.cart_items
                        .ToList()
                        .Select(item => new cart_items
                        {
                            cart_item_id = item.cart_item_id,
                            product = item.product,
                            total_price = item.total_price,
                            quantity = item.quantity
                        })
                        .ToList()
                    };
                    return View(cartTemp);
                }
            }
            return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            if (Session["user"] == null) return RedirectToAction("Index", "Login");
            using (var ctx = new DBContext())
            {
                var pid = Convert.ToInt32(form["pid"] ?? "0");
                var price = Convert.ToDecimal(form["price"]);
                var quantity = Convert.ToInt32(form["quantity"] ?? "1");
                var user = Session["user"] as user;
                var cartByUser = ctx.carts.FirstOrDefault(item => item.user_id == user.user_id);
                if (cartByUser != null)
                {
                    var cartItem = ctx.cart_items.FirstOrDefault(item => item.cart_id == cartByUser.cart_id && item.product_id == pid);
                    if (cartItem != null)
                    {
                        cartItem.quantity += quantity;
                        cartItem.total_price = cartItem.quantity * price;
                        cartItem.updated_at = DateTime.Now;
                    }
                    else
                    {
                        cartByUser.cart_items.Add(new cart_items
                        {
                            cart_id = cartByUser.cart_id,
                            product_id = pid,
                            quantity = quantity,
                            total_price = price * quantity,
                            created_at = DateTime.Now,
                            updated_at = DateTime.Now
                        });
                    }
                    cartByUser.total_quantity += quantity;
                    cartByUser.updated_at = DateTime.Now;
                    ctx.SaveChanges();
                    cartByUser.total_price = ctx.cart_items.Where(item => item.cart_id == cartByUser.cart_id).Sum(item => item.total_price);
                    cartByUser.total_quantity = ctx.cart_items.Where(item => item.cart_id == cartByUser.cart_id).Sum(item => item.quantity);
                }
                else
                {
                    var newCart = new cart
                    {
                        user_id = user.user_id,
                        total_price = price * quantity,
                        total_quantity = quantity,
                        updated_at = DateTime.Now,
                        created_at = DateTime.Now,
                    };
                    newCart.cart_items.Add(new cart_items
                    {
                        cart = newCart,
                        product_id = pid,
                        quantity = quantity,
                        total_price = price * quantity,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    });
                    ctx.carts.Add(newCart);
                }
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult updateCart(int quantity, int pid)
        {
            using (var ctx = new DBContext())
            {
                var user = Session["user"] as user;
                var cartByUser = ctx.carts.FirstOrDefault(item => item.user_id == user.user_id);
                if (cartByUser != null)
                {
                    var cartItem = ctx.cart_items.FirstOrDefault(item => item.cart_id == cartByUser.cart_id && item.product_id == pid);
                    if (cartItem != null)
                    {
                        cartItem.quantity = quantity;
                        cartItem.total_price = cartItem.quantity * cartItem.product.price;
                        cartItem.updated_at = DateTime.Now;
                    }
                    ctx.SaveChanges();
                    cartByUser.updated_at = DateTime.Now;
                    cartByUser.total_price = ctx.cart_items.Where(item => item.cart_id == cartByUser.cart_id).Sum(item => item.total_price);
                    cartByUser.total_quantity = ctx.cart_items.Where(item => item.cart_id == cartByUser.cart_id).Sum(item => item.quantity);
                    ctx.SaveChanges();
                }
            }
            return Json(new { });
        }

        public ActionResult Remove(int id)
        {
            using (var ctx = new DBContext())
            {
                var cartItem = ctx.cart_items.FirstOrDefault(r => r.cart_item_id == id);
                if (cartItem != null)
                {
                    ctx.cart_items.Remove(cartItem);
                    ctx.SaveChanges();
                }
                var user = Session["user"] as user;
                var cartByUser = ctx.carts.FirstOrDefault(item => item.user_id == user.user_id);
                cartByUser.updated_at = DateTime.Now;
                cartByUser.total_price = ctx.cart_items.Where(item => item.cart_id == cartByUser.cart_id).Sum(item => item.total_price);
                cartByUser.total_quantity = ctx.cart_items.Where(item => item.cart_id == cartByUser.cart_id).Sum(item => item.quantity);
                ctx.SaveChanges();
            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult Checkout(FormCollection form)
        {
            using(var ctx = new DBContext())
            {
                var user = Session["user"] as user;
                var cartByUser = ctx.carts.FirstOrDefault(item => item.user_id == user.user_id);
                var o = new order();
                o.user_id = user.user_id;
                o.shipping_address = form["address"].ToString();
                o.phone_number = form["phone"].ToString();
                o.total_price = cartByUser.total_price;
                o.total_quantity = cartByUser.total_quantity;
                o.created_at = DateTime.Now;
                o.updated_at = DateTime.Now;
                o.status = "pending";
                o.full_name = form["fullname"].ToString();
                cartByUser.cart_items.ForEach(item =>
                {
                    o.order_items.Add(new order_items
                    {
                        order = o,
                        product_id = item.product_id,
                        quantity = item.quantity,
                        total_price = item.total_price,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now,
                    });
                });
                ctx.orders.Add(o);
                ctx.cart_items.RemoveRange(cartByUser.cart_items);
                cartByUser.total_price = 0;
                cartByUser.total_quantity = 0;
                cartByUser.cart_items = null;
                cartByUser.updated_at = DateTime.Now;
                ctx.SaveChanges();
                Session["order"] = "Đặt hàng thành công";
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}