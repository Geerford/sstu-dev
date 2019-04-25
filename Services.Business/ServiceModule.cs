using Infrastructure.Data;
using Ninject.Modules;
using Repository.Interfaces;

namespace Services.Business
{
    /// <summary>
    /// Implements <see cref="NinjectModule"/>.
    /// </summary>
    public class ServiceModule : NinjectModule
    {
        /// <summary>
        /// Store for the Name property.
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Store for the Name property.
        /// </summary>
        private readonly string connectionStringSync;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceModule"/> class.
        /// </summary>
        /// <param name="connection">The connection string.</param>
        public ServiceModule(string connectionString, string connectionStringSync)
        {
            this.connectionString = connectionString;
            this.connectionStringSync = connectionStringSync;
        }

        /// <summary>
        /// Binds the <see cref="UnitOfWork"/> interfaces. 
        /// </summary>
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
            Bind<ISyncUnitOfWork>().To<SyncUnitOfWork>().WithConstructorArgument(connectionStringSync);
        }
    }
}