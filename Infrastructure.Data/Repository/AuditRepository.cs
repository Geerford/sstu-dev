using Domain.Contracts;
using Domain.Core.Logs;
using System.Collections.Generic;

namespace Infrastructure.Data.Repository
{
    /// <summary>
    /// Implements <see cref="IAuditRepository"/>.
    /// </summary>
    public class AuditRepository : IAuditRepository<Audit>
    {
        /// <summary>
        /// Store for the <see cref="Context"/> property.
        /// </summary>
        private Context database;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> object.</param>
        public AuditRepository(Context context)
        {
            database = context;
        }

        /// <summary>
        /// Implements <see cref="IAuditRepository.Create(Audit)"/>.
        /// </summary>
        public void Create(Audit item)
        {
            database.Audits.Add(item);
        }

        /// <summary>
        /// Implements <see cref="IAuditRepository.GetAll"/>.
        /// </summary>
        public IEnumerable<Audit> GetAll()
        {
            return database.Audits;
        }
    }
}