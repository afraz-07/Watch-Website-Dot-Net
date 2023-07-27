using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Watch_Website.Models;

namespace Watch_Website.Controllers
{
    public class ProductController : Controller
    {
        WatchEntities DB = new WatchEntities();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateProduct()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product p)
        {
            string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);

            string extension = Path.GetExtension(p.ImageFile.FileName);

            filename = filename +  extension;

            p.Image = "~/Images/" + filename;

            filename = Path.Combine(Server.MapPath("~/Images/")+ filename);

            p.ImageFile.SaveAs(filename);

            using (WatchEntities w = new WatchEntities())
            {
                w.Products.Add(p);
                w.SaveChanges();
            }
            return RedirectToAction("Index" , "Home");

        }


        public ActionResult ProductList()
        {
            var imageList = DB.Products.ToList();

            return View(imageList); 
        }

        public ActionResult ProductDetails(int id)
        {
            var imageModel = DB.Products.FirstOrDefault(img => img.Id == id);

            if (imageModel != null)
            {
                return View(imageModel);
            }

            return HttpNotFound(); // Or any other appropriate response if the image is not found.

        }

        public ActionResult EditProduct(int id)
        {
            var find = DB.Products.Find(id);
            return View(find);
        }

        [HttpPost]
        public ActionResult EditProduct(Product p)
        {
            DB.Entry(p).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();
            return RedirectToAction("ProductList");
        }

        public ActionResult DeleteProduct(int id)
        {
            var del = DB.Products.Find(id);
            return View(del);
        }

        [HttpPost]
        public ActionResult DeleteProduct(Product p)
        {
            DB.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            DB.SaveChanges();
            return RedirectToAction("ProductList");
        }
    }
}