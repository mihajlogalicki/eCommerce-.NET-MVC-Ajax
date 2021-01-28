using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineShop.ApiServices
{
    public sealed class ProductServices
    {

        private static ProductServices _instance = null;

        private ProductServices() {}

        public static ProductServices GetSingleInstance()
        {
            if (_instance == null)
            {
                _instance = new ProductServices();
            }
            return _instance;
        }

        public void SaveProduct(Product product)
        {
            using (var context = new DatabaseContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public List<Product> GetAllProducts(int pageNo)
        {
            int pageSize = 3;
            using (var context = new DatabaseContext())
            {
                return context.Products.OrderBy(x => x.Id).Skip((pageNo-1)*pageSize).Take(pageSize).Include(c => c.Category).ToList();
            }
        }

        public Product GetProductById(int product)
        {
            using (var context = new DatabaseContext())
            {
                return context.Products.Find(product);
            }
        }

        public int GetProductsCount()
        {
            using (var context = new DatabaseContext())
            {
                return context.Products.Count();
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