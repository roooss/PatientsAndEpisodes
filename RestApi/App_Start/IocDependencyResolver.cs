using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Web.Http.Dependencies;

namespace RestApi.App_Start
{
    internal class IocDependencyResolver : IDependencyResolver
    {
        private IUnityContainer Container { get; set; }

        public IocDependencyResolver(IUnityContainer container)
        {
            this.Container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this.Container.Resolve(serviceType);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.Container.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = this.Container.CreateChildContainer();
            return new IocDependencyResolver(child);
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }
    }
}