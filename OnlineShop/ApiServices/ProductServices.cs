using OnlineShop.Database;
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
                return context.Products.Include(c => c.Category).ToList();
            }
        }

        public Product GetProductById(int product)
        {
            using (var context = new DatabaseContext())
            {
                return context.Products.Find(product);
            }
        }

        public List<Product> GetProductsByIDs(List<int> IDs)
        {
            using (var context = new DatabaseContext())
            {
                return context.Products.Where(product => IDs.Contains(product.Id)).ToList();
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