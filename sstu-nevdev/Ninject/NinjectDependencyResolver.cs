using Ninject;
using Service.Interfaces;
using Services.Business.Services;
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
            kernel.Bind<IAdmissionService>().To<AdmissionService>();
            kernel.Bind<ICheckpointAdmissionService>().To<CheckpointAdmissionService>();
            kernel.Bind<ICheckpointService>().To<CheckpointService>();
            kernel.Bind<IIdentityService>().To<IdentityService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<ITypeService>().To<TypeService>();
        }
    }
}