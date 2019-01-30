using System.Collections.Generic;

namespace Domain.Contracts
{
    /// <summary>
    /// Represents to a storage location for safety or preservation.
    /// </summary>
    /// <typeparam name="T">The <see cref="IAuditRepository{T}"/> repository.</typeparam>
    public interface IAuditRepository<T> where T : class
    {
        /// <summary>
        /// Gets all entry from the repository.
        /// </summary>
        /// <returns>The all entry from the repository.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Creates the <see cref="T"/> entry in the repository.
        /// </summary>
        /// <param name="item">The <see cref="T"/> entry.</param>
        void Create(T item);
    }
}