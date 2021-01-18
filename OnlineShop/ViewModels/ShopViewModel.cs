using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.ViewModels
{
    public class ShopViewModel
    {
        public List<Product> CartProducts { get; set; }
        public  List<int> CartProudctsIDs { get; set; }
    }
}