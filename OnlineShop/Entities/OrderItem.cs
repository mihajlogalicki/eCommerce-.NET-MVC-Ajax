using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; } 

        public int productId { get; set; }
        public virtual Product Product { get; set; }
    }
}