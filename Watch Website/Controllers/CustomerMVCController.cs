using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Watch_Website.Models;

namespace Watch_Website.Controllers
{
    public class CustomerMVCController : Controller
    {
        HttpClient client = new HttpClient();
        public ActionResult CustomerRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomerRegister(Customer c)
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

        public ActionResult EditCustomerProfile(int id)
        {
            Customer c = null;
            client.BaseAddress = new Uri("http://localhost:50697/api/Customerwebapi");
            var response = client.GetAsync("WebApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Customer>();
                display.Wait();
                c = display.Result;
            }

            return View(c);

        }

        [HttpPost]
        public ActionResult EditCustomerProfile(Customer c)
        {
            client.BaseAddress = new Uri("http://localhost:50697/api/Customerwebapi");
            var response = client.PutAsJsonAsync("WebApi", c);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }

            return View("Edit");

        }
    }
}