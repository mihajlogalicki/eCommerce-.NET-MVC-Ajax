using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.ViewModels
{
    public class WidgetViewModels
    {
        public List<Product> Products { get; set; }
        public bool IsLatestProduct { get; set; }
    }
}