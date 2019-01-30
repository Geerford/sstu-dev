using Domain.Core;
using System.Data.Common;
using System.Data.Entity;

namespace Infrastructure.Data
{
    /// <summary>
    /// Implements <see cref="DbContext"/>.
    /// </summary>
    public class SyncContext : DbContext
    {
        /// <summary>
        /// Gets or sets the <see cref="User"/> <see cref="DbSet"/>.
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Initializes a <see cref="Database"/> of new data.
        /// </summary>
        static SyncContext()
        {
            Database.SetInitializer(new SyncInitializer());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SyncContext(string connectionString) : base(connectionString) { }

        /// <summary>
        /// For unit testing.
        /// </summary>
        public SyncContext(DbConnection connection) : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}