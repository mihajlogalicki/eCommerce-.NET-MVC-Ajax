using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.ViewModels
{
    public class ProductViewModel
    {
        public int PageNo { get; set; }
        public int TotalPages { get; set; }
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }
    }
}