namespace RestApi.App_Start
{
    using Microsoft.Practices.Unity;
    using RestApi.Interfaces;
    using RestApi.Models;
    using System.Web.Http.Dependencies;
    public static class IocContainer
    {
        public static IUnityContainer ConfigureIocContainer()
        {
            // Get our IoC Container
            IUnityContainer container = new UnityContainer();

            // Register any custom components we know we have.
            registerComponents(container);

            return container;
        }

        public static IDependencyResolver GetDependencyResolver(IUnityContainer container)
        { 
            // Return our custom unity container dependency resolver for web api to work with.
            return new IocDependencyResolver(container);
        }

        private static void registerComponents(IUnityContainer container)
        {
            container.RegisterType<IDatabaseContext, PatientContext>();
        }
    }
}