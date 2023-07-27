using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Watch_Website.Models;

namespace Watch_Website.Controllers
{
    public class HomeController : Controller
    {
        WatchEntities DB = new WatchEntities();

        public ActionResult Index( string searchtext)
        {
            var imageList = DB.Products.ToList();

            if (searchtext != null)
            {
                imageList = DB.Products.Where(x => x.Name.Contains(searchtext) || x.Brand.Contains(searchtext) ).ToList();

                string dn = "d-none";

                ViewBag.None = dn;

            }

            return View(imageList);
        }

        public ActionResult ViewDetails(int id , decimal p )
        {

            var imageModel = DB.Products.FirstOrDefault(img => img.Id == id);

            decimal pr = p;

            ViewBag.Price = string.Format("{0:N}" , pr);

            return View(imageModel);

        }

        
    }
}
