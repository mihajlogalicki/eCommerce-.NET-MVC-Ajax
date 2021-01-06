using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}