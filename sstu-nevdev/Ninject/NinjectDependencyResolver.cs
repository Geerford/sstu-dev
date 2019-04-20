using Ninject;
using Service.Interfaces;
using Services.Business.Service;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace sstu_nevdev.Ninject
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IActivityService>().To<ActivityService>();
            kernel.Bind<IAuditService>().To<AuditService>();
            kernel.Bind<IAdmissionService>().To<AdmissionService>();
            kernel.Bind<ICheckpointService>().To<CheckpointService>();
            kernel.Bind<IDatabaseService>().To<DatabaseService>();
            kernel.Bind<IIdentityService>().To<IdentityService>();
            kernel.Bind<IModeService>().To<ModeService>();
            kernel.Bind<ITypeService>().To<TypeService>();
        }
    }
}