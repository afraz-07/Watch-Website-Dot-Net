using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Watch_Website.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public int Quantity { get; set; }

    }
}