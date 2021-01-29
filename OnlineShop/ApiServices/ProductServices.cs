using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

        public List<Product> GetAllProducts(string search, int pageNo)
        {
            int pageSize = 3;
            using (var context = new DatabaseContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(p => p.Name.ToLower().Contains(search))
                         .OrderBy(x => x.Id)
                         .Skip((pageNo - 1) * pageSize)
                         .Take(pageSize)
                         .Include(c => c.Category)
                         .ToList();
                } else
                {
                    return context.Products.OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(c => c.Category)
                        .ToList();
                }
            }
        }

        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new DatabaseContext())
            {
                return context.Products.OrderByDescending(x => x.Id)
                        .Take(numberOfProducts)
                        .Include(c => c.Category)
                        .ToList();
            }
        }

        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            using (var context = new DatabaseContext())
            {
                return context.Products
                     .OrderByDescending(x => x.Id)
                     .Skip((pageNo - 1) * pageSize)
                     .Take(pageSize)
                     .Include(c => c.Category)
                     .ToList();
            }
        }

        public Product GetProductById(int product)
        {
            using (var context = new DatabaseContext())
            {
                return context.Products.Include(p => p.Category).FirstOrDefault(x => x.Id == product);
            }
        }

        public int GetProductsCount(string search)
        {
            using (var context = new DatabaseContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(p => p.Name.ToLower().Contains(search))
                              .Include(c => c.Category)
                              .Count();
                }
                else
                {
                    return context.Products.Count();
                }
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