using OnlineShop.Models;
using OnlineShop.Modules;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

namespace OnlineShop
{
    public class zModel : DbContext
    {
        /*
         * Add-Migration db1 -context zModel
         * Update-Database -context zModel
         * Remove-Migration -context zModel
         */

        public DbSet<eProduct> eProducts { get; set; }

        protected zModel() : base(clsGeneral.connectionString ?? "OnlineShopDbContext")
        {
        }

        public zModel(string nameOrConnectionString) : base(clsGeneral.connectionString ?? "OnlineShopDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<eProduct>().HasKey(x => x.KeyID);
        }
    }

    public class MigrationsContextFactory : IDbContextFactory<zModel>
    {
        public zModel Create()
        {
            return new zModel(clsGeneral.connectionString ?? "OnlineShopDbContext");
        }
    }

    public class CustomConfiguration : DbMigrationsConfiguration<zModel>
    {
        public CustomConfiguration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(zModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}