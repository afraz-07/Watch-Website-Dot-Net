using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Watch_Website.Models;

namespace Watch_Website.Controllers
{
    public class CustomerWebApiController : ApiController
    {

        WatchEntities DB = new WatchEntities();

        [HttpPost]
        public IHttpActionResult Create(Customer c)
        {
            DB.Customers.Add(c);
            DB.SaveChanges();

            return Ok();
        }


        [HttpPut]
        public IHttpActionResult Edit(Customer c) 
        {
            DB.Entry(c).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();

            return Ok();
        }
        

    }
}
