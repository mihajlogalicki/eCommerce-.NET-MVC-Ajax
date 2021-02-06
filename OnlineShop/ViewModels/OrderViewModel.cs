using OnlineShop.Entities;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public Order Order { get; set; }

        public string UserId { get; set; }
        public ApplicationUser OrderByUser { get; set; }

        public string Status { get; set; }
        public List<string> AvailableStatus { get; set; }

        public int Pager { get; set; }
        public int TotalPages { get; set; }
        public int TotalProductCount { get; set; }
    }
}