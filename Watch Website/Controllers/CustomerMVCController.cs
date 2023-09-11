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
    public class CustomerMVCController : Controller
    {
        

        WatchEntities DB = new WatchEntities();
        HttpClient client = new HttpClient();
        public ActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserRegister(Customer c)
        {
            client.BaseAddress = new Uri("http://localhost:50697/api/Customerwebapi");
            var response = client.PostAsJsonAsync("Customerwebapi", c);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Create");
        }

        [Authorize]
        public ActionResult BuyNow (string Mobile , string pname , string pmodel , string pbrand , decimal pprice ,string pimg)
        {

            var f = DB.Customers.Where(x=>x.MobileNo == Mobile).FirstOrDefault();


            ViewBag.ProName = pname;
            ViewBag.ProModel = pmodel;
            ViewBag.ProBrand = pbrand;
            ViewBag.ProPrice = pprice;
            ViewBag.ProImg = pimg;

           

            return View(f);
        }

        [HttpPost]
        public ActionResult BuyNow(Customer c)
        {
            DB.Entry(c).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("PlaceOrder" , "CustomerMVC");
        }

        public ActionResult PlaceOrder()
        {
            return View();
        }
       
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(Customer c)
        {
            try
            {
                var user = DB.Customers.Where(x => x.MobileNo == c.MobileNo && x.Password == c.Password).FirstOrDefault();

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(c.MobileNo, true);
                    return RedirectToAction("Index", "Home");
                }

               

                return View();
            }
            catch 
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index" , "Home");
        }

        public ActionResult UserList()
        {
            var list = DB.Customers.ToList();
            return View(list);
        }
    }
}