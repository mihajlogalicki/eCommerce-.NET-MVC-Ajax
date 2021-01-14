using OnlineShop.Database;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineShop.ApiServices
{
    public class CategoryServices
    {
        public void SaveCategory(Category category)
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

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new DatabaseContext())
            {
                return context.Categories.Where(c => c.IsFeatured && c.ImageUrl != null).ToList();
            }
        }

        public Category GetCategoryById(int category)
        {
            using (var context = new DatabaseContext())
            {
                return context.Categories.Find(category);
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new DatabaseContext())
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(Category category)
        {
            using (var context = new DatabaseContext())
            {
                context.Entry(category).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

    }
}