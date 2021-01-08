﻿using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineShop.ApiServices
{
    public class ProductServices
    {
        public void SaveProduct(Product product)
        {
            using (var context = new DatabaseContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public List<Product> GetAllProducts()
        {
            using (var context = new DatabaseContext())
            {
                return context.Products.ToList();
            }
        }

        public Product GetProductById(int product)
        {
            using (var context = new DatabaseContext())
            {
                return context.Products.Find(product);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new DatabaseContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(Product product)
        {
            using (var context = new DatabaseContext())
            {
                context.Entry(product).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}