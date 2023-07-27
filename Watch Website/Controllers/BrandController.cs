using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Watch_Website.Models;

namespace Watch_Website.Controllers
{
    public class BrandController : Controller
    {
        WatchEntities DB = new WatchEntities();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CreateBrand()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateBrand(Brand brand)
        {
            string filename = Path.GetFileNameWithoutExtension(brand.ImageFile.FileName);

            string extension = Path.GetExtension(brand.ImageFile.FileName);

            filename = filename + extension;

            brand.Image = "~/Images/" + filename;

            filename = Path.Combine(Server.MapPath("~/Images/") + filename);

            brand.ImageFile.SaveAs(filename);

            using (WatchEntities w = new WatchEntities())
            {
                w.Brands.Add(brand);
                w.SaveChanges();
            }
            return RedirectToAction("ProductList");

        }

        public ActionResult TopBrands()
        {
            var imageList = DB.Brands.ToList();

            return View(imageList);
        }

        public ActionResult DisplayBrand(string b ) 
        {
            var brands = DB.Products.Where(x => x.Brand == b).ToList();

            ViewBag.Brands = b;

            return View(brands);

        }
    }
}