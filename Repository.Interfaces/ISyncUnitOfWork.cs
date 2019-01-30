using Domain.Contracts;
using Domain.Core;
using System;

namespace Repository.Interfaces
{
    /// <summary>
    /// Unit of work interface. Maintains a list of objects affected by a 
    /// business transaction and coordinates the writing out of changes.
    /// </summary>
    public interface ISyncUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="User"/> repository.
        /// </summary>
        ISyncRepository<User> User { get; }
    }
}
