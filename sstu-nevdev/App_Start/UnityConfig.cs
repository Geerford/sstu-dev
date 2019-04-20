using Infrastructure.Data;
using Repository.Interfaces;
using Service.Interfaces;
using Services.Business.Service;
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
            var connectionStringSync = System.Configuration.ConfigurationManager.ConnectionStrings["SyncContext"].ConnectionString;
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor(connectionString));
            container.RegisterType<ISyncUnitOfWork, SyncUnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor(connectionStringSync));
            container.RegisterType<IActivityService, ActivityService>();
            container.RegisterType<IAdmissionService, AdmissionService>();
            container.RegisterType<IAESService, AESService>();
            container.RegisterType<IAuditService, AuditService>();
            container.RegisterType<ICheckpointService, CheckpointService>();
            container.RegisterType<IDatabaseService, DatabaseService>();
            container.RegisterType<IIdentityService, IdentityService>();
            container.RegisterType<ITypeService, TypeService>();
            return container;
        }
    }
}