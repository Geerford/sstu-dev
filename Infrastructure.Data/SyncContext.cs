using Domain.Core;
using System.Data.Entity;

namespace Infrastructure.Data
{
    public class SyncContext : DbContext
    {
        public DbSet<User> User { get; set; }

        static SyncContext()
        {
            Database.SetInitializer(new SyncInitializer());
        }
        public SyncContext(string connectionString) : base(connectionString) { }
    }
}