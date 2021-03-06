﻿using OnlineShop.Entities;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.ViewModels
{
    public class ShopViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProudctsIDs { get; set; }

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public decimal maximumPrice { get; set; }
        public int? CategoryId { get; set; }
        public int Pager { get; set; }
        public int PageSize { get; set; } = 6;
        public int TotalPages { get; set; }
        public int TotalProductCount {get; set;}

        public ApplicationUser User { get; set; }
    }
}