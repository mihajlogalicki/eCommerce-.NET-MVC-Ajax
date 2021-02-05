using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.ApiServices
{
    public class ShopService
    {

        private static ShopService _instance = null;

        private ShopService() { }

        public static ShopService GetSingleInstance()
        {
            if (_instance == null)
            {
                _instance = new ShopService();
            }
            return _instance;
        }

        public int SaveOrder(Order order)
        {
            using (var context = new DatabaseContext())
            {
                context.Orders.Add(order);
                return context.SaveChanges();
            }
        }
    }
}