using Domain.Core.Logs;
using System.Collections.Generic;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface IAuditService
    {
        /// <summary>
        /// Performs <see cref="UnitOfWork.Dispose()"/>.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Gets all <see cref="Audit"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="Audit"/> from the repository.</returns>
        IEnumerable<Audit> GetAll();
    }
}