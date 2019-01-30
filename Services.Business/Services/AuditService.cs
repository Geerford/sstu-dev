using Domain.Core.Logs;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;

namespace Services.Business.Services
{
    /// <summary>
    /// Implements <see cref="IAuditService"/>.
    /// </summary>
    public class AuditService : IAuditService
    {
        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditService"/> class.
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> object.</param>
        public AuditService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Implements <see cref="IAuditService.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>
        /// Implements <see cref="IAuditService.Dispose"/>.
        /// </summary>
        public IEnumerable<Audit> GetAll()
        {
            return Database.Audit.GetAll();
        }
    }
}