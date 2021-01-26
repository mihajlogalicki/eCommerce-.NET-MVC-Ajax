using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShop.Database;
using OnlineShop.Entities;

namespace OnlineShop.ApiServices
{
    public class ConfigurationService
    {
        public Config GetConfig(string key)
        {
            using (var context = new DatabaseContext())
            {
                return context.Configurations.Find(key);
            }
        }
    }
}