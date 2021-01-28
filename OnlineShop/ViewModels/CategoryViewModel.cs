using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.ViewModels
{
    public class CategoryViewModel
    {
        public int PageNo { get; set; }
        public List<Category> Categories { get; set; }
        public string Search { get; set; }
    }
}