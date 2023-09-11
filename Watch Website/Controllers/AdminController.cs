using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Watch_Website.Models;

namespace Watch_Website.Controllers
{
   
    public class AdminController : Controller
    {
        WatchEntities DB = new WatchEntities();

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin a)
        {
            var AdminUser = DB.Admins.Where(x=> x.UserId == a.UserId && x.Password == a.Password).FirstOrDefault();

            if (AdminUser != null)
            {
                FormsAuthentication.SetAuthCookie(a.UserId, false);
                return RedirectToAction("AdminHome");
            }
            return View();
        }


        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int id) 
        {
            var find = DB.Admins.Find(id);
            return View(find);
        }

        [HttpPost]
        public ActionResult Edit(Admin a)
        {
            DB.Entry(a).State = System.Data.Entity.EntityState.Modified; 
            DB.SaveChanges();
            return RedirectToAction("ProductList", "Product");
        }


        [Authorize(Roles = "Admin")]

        public ActionResult AdminHome()
        {
            return View();
        }

    }
}