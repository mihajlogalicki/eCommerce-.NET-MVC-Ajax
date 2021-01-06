using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.ApiServices
{
    public class CategoryServices
    {
        public void CreateCategory(Category category)
        {
            using (var context = new DatabaseContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public List<Category> GetAllCategories()
        {
            using (var context = new DatabaseContext())
            {
                return context.Categories.ToList();
            }
        }
    }
}