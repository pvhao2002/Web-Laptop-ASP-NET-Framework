﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebBanLaptop.Models;

namespace WebBanLaptop.Areas.Admin.Controllers
{
    public class UserAdminController : Controller
    {
        // GET: Admin/UserAdmin
        public ActionResult Index()
        {
            using (var ctx = new DBContext())
            {
                var list = ctx.users.ToList();
                return View(list);
            }
        }
        public ActionResult Block(int id)
        {
            using (var ctx = new DBContext())
            {
                var user = ctx.users.FirstOrDefault(item => item.user_id == id);
                if (user != null)
                {
                    user.status = "block";
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult unBlock(int id)
        {
            using (var ctx = new DBContext())
            {
                var user = ctx.users.FirstOrDefault(item => item.user_id == id);
                if (user != null)
                {
                    user.status = "active";
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}