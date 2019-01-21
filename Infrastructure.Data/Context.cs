using Domain.Core;
using Domain.Core.Logs;
using System.Data.Common;
using System.Data.Entity;

namespace Infrastructure.Data
{
    public class Context : DbContext
    {
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Admission> Admission { get; set; }
        public DbSet<Checkpoint> Checkpoint { get; set; }
        public DbSet<CheckpointAdmission> CheckpointAdmission { get; set; }
        public DbSet<Identity> Identity { get; set; }
        public DbSet<Type> Type { get; set; }

        /// <summary>
        /// Logs
        /// </summary>
        public DbSet<Audit> Audits { get; set; }

        static Context()
        {
            Database.SetInitializer(new Initializer());
        }
        public Context(string connectionString) : base(connectionString) { }

        /// <summary>
        /// For unit testing
        /// </summary>
        public Context(DbConnection connection) : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}