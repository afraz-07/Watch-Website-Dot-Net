using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Watch_Website.Models;

namespace Watch_Website.Controllers
{
    public class CartController : Controller
    {
        private List<Cart> GetCart()
        {
            // Retrieve the cart from session or create a new one if it doesn't exist
            var cart = Session["Cart"] as List<Cart>;
            if (cart == null)
            {
                cart = new List<Cart>();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private decimal CalculateTotalPrice(List<Cart> cartItems)
        {
            decimal total = 0;
            foreach (var item in cartItems)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }

        public ActionResult MyCart()
        {
            var cart = GetCart();

            decimal totalPrice = CalculateTotalPrice(cart);
            ViewBag.TotalPrice = totalPrice;

            return View(cart);
        }

        public ActionResult AddToCart(int id, string name, string model , string brand , decimal price , string img)
        {
            var cart = GetCart();

            // Check if the product already exists in the cart
            var existingItem = cart.FirstOrDefault(item => item.Id == id);


            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var newItem = new Cart
                {
                    Id = id,
                    Name = name,
                    Model = model,
                    Brand = brand,
                    Price = price,
                    Image = img,
                    Quantity = 1

                };
                cart.Add(newItem);
            }
            

            return RedirectToAction("MyCart");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();

            // Find the item with the specified productId in the cart
            var itemToRemove = cart.FirstOrDefault(item => item.Id == id);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }

            return RedirectToAction("MyCart");
        }

        //public ActionResult BuyNow(int id , Product p)
        //{


        //    return View();
        //}
    }

}