using Infrastructure.Data;
using Repository.Interfaces;
using Service.Interfaces;
using Services.Business.Services;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace sstu_nevdev.App_Start
{
    public static class UnityConfig
    {
        public static IUnityContainer GetConfiguredContainer()
        {
            var container = new UnityContainer();
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor(connectionString));
            container.RegisterType<IRoleService, RoleService>();
            return container;
        }
    }
}