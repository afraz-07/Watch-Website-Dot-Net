﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Watch_Website.Models;

namespace Watch_Website.Controllers
{
   

    [Authorize]

    
    public class CartController : Controller
    {

        WatchEntities DB = new WatchEntities();

        private List<Cart> GetCart()
        {
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
            

            string format = totalPrice.ToString("N");

            ViewBag.TotalPrice = format;




            return View(cart);
        }

       

        public ActionResult AddToCart(int id, string name, string model , string brand , decimal price , string img )
        {
            var cart = GetCart();

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

            var itemToRemove = cart.FirstOrDefault(item => item.Id == id);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }

            return RedirectToAction("MyCart");
        }

        public ActionResult BuyNow()
        {
            var model1 = new Customer(); 
            var model2 = new Product(); 

            var viewModel = new cartbuy
            {
                customer = model1,
                product = model2
            };

            //return View(viewModel);

            var cart = GetCart();

            decimal totalPrice = CalculateTotalPrice(cart);


            string format = totalPrice.ToString("N");

            ViewBag.TotalPrice = format;




            return View(cart);
        }


    }

}