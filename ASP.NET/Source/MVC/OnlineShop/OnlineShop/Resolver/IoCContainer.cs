using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace OnlineShop.Resolver
{
    public class IoCContainer : ScopeContainer, System.Web.Mvc.IDependencyResolver
    {
        public IoCContainer(IUnityContainer container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new ScopeContainer(child);
        }
    }
}