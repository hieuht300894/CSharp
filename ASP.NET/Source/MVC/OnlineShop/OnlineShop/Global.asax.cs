using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity.Lifetime;

namespace OnlineShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.GetConnectionString();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnityMvcActivator.Start();
            UnityConfig.Container.RegisterType(typeof(IUnitOfWork), typeof(UnitOfWork), "UnitOfWork", new HierarchicalLifetimeManager());
        }
    }
}
