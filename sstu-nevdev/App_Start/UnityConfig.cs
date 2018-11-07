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
            container.RegisterType<IActivityService, ActivityService>();
            container.RegisterType<IAdmissionService, AdmissionService>();
            container.RegisterType<ICheckpointAdmissionService, CheckpointAdmissionService>();
            container.RegisterType<ICheckpointService, CheckpointService>();
            container.RegisterType<IIdentityService, IdentityService>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<ITypeService, TypeService>();
            return container;
        }
    }
}