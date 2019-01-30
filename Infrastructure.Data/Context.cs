using Domain.Core;
using Domain.Core.Logs;
using System.Data.Common;
using System.Data.Entity;

namespace Infrastructure.Data
{
    /// <summary>
    /// Implements <see cref="DbContext"/>.
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// Gets or sets the <see cref="Activity"/> <see cref="DbSet"/>.
        /// </summary>
        public DbSet<Activity> Activity { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Admission"/> <see cref="DbSet"/>.
        /// </summary>
        public DbSet<Admission> Admission { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Audits"/> <see cref="DbSet"/>.
        /// </summary>
        public DbSet<Audit> Audits { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Checkpoint"/> <see cref="DbSet"/>.
        /// </summary>
        public DbSet<Checkpoint> Checkpoint { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CheckpointAdmission"/> <see cref="DbSet"/>.
        /// </summary>
        public DbSet<CheckpointAdmission> CheckpointAdmission { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Identity"/> <see cref="DbSet"/>.
        /// </summary>
        public DbSet<Identity> Identity { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> <see cref="DbSet"/>.
        /// </summary>
        public DbSet<Type> Type { get; set; }

        /// <summary>
        /// Initializes a <see cref="Database"/> of new data.
        /// </summary>
        static Context()
        {
            Database.SetInitializer(new Initializer());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public Context(string connectionString) : base(connectionString) { }

        /// <summary>
        /// For unit testing.
        /// </summary>
        public Context(DbConnection connection) : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}