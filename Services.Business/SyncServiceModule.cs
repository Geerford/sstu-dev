using Infrastructure.Data;
using Ninject.Modules;
using Repository.Interfaces;

namespace Services.Business
{
    public class SyncServiceModule : NinjectModule
    {
        private readonly string connectionString;
        
        public SyncServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<ISyncUnitOfWork>().To<SyncUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
