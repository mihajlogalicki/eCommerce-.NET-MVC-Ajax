using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

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

        public List<Order> SearchOrders(string status)
        {
            using (var context = new DatabaseContext())
            {
                var orders = context.Orders.OrderBy(x => x.Id).ToList();

                if (!string.IsNullOrEmpty(status))
                {
                    orders = context.Orders.Where(order => order.Status.Contains(status.ToLower())).ToList();
                }

                return orders.ToList();
            }
        }

        public Order GetOrderById(int? id)
        {
            using (var context = new DatabaseContext())
            {
                return context.Orders.Where(x => x.Id == id)
                    .Include(x => x.OrderItems)
                    .Include("OrderItems.Product")
                    .FirstOrDefault();
            }
        }

        public object UpdateOrderStatus(string status, int id)
        {
            using (var context = new DatabaseContext())
            {
                var order = context.Orders.Find(id);
                order.Status = status;
                context.Entry(order).State = EntityState.Modified;
                return context.SaveChanges();
            }
        }
    }
}