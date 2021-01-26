using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShop.Database;
using OnlineShop.Entities;

namespace OnlineShop.ApiServices
{
    public sealed class ConfigurationService
    {
        private static ConfigurationService _instance = null;

        private ConfigurationService(){}

        public static ConfigurationService GetSingleInstance()
        {
            if(_instance == null)
            {
                _instance = new ConfigurationService();
            }
            return _instance;
        }

        public Config GetConfig(string key)
        {
            using (var context = new DatabaseContext())
            {
                return context.Configurations.Find(key);
            }
        }
    }
}