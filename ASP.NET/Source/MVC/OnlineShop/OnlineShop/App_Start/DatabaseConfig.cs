using OnlineShop.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineShop
{
    public class DatabaseConfig
    {
        public static void GetConnectionString()
        {
            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["OnlineShopDbContext"];
            clsGeneral.connectionString = setting != null ? setting.ConnectionString : string.Empty;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<zModel, CustomConfiguration>());
        }
    }
}