using sstu_nevdev.App_Start;
using System.Web.Http;

using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(sstu_nevdev.UnityWebApiActivator), nameof(sstu_nevdev.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(sstu_nevdev.UnityWebApiActivator), nameof(sstu_nevdev.UnityWebApiActivator.Shutdown))]

namespace sstu_nevdev
{
    public static class UnityWebApiActivator
    {
        public static void Start() 
        {
            var resolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static void Shutdown()
        {
            UnityConfig.GetConfiguredContainer().Dispose();
        }
    }
}