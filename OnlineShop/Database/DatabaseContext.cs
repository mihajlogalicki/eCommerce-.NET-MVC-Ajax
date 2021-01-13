using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineShop.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("MultiWebShop") {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}