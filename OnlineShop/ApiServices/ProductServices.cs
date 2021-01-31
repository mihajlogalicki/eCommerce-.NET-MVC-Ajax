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

        public List<Product> SearchProducts(string search, int pageNo, int? minPrice, int? maxPrice, int? categoryId, int? sortBy)
        {
            int pageSize = 6;
            using (var context = new DatabaseContext())
            {
                var products = context.Products.OrderBy(x => x.Id).ToList();

                if (categoryId.HasValue)
                {
                    products = context.Products.Where(c => c.Category.Id == categoryId.Value).ToList();
                }

                if(!string.IsNullOrEmpty(search))
                {
                    products = context.Products.Where(product => product.Name.Contains(search.ToLower())).ToList();
                } 

                if (minPrice.HasValue)
                {
                    products = products.Where(product => product.Price >= minPrice.Value).ToList();
                }

                if (maxPrice.HasValue)
                {
                    products = products.Where(product => product.Price <= maxPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = context.Products.OrderByDescending(x => x.Id).ToList();
                        break;
                        case 3:
                            products = context.Products.OrderBy(x => x.Price).ToList();
                        break;
                        case 4:
                            products = context.Products.OrderByDescending(x => x.Price).ToList();
                        break;
                    }
                }

                return products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
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

        public decimal GetMaximumPrice()
        {
            using (var context = new DatabaseContext())
            {
                return (int)(context.Products.Max(x => x.Price));
            }
        }
    }
}