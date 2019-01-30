using System;
using System.Collections.Generic;

namespace Domain.Contracts
{
    /// <summary>
    /// Represents to a storage location for safety or preservation.
    /// </summary>
    /// <typeparam name="T">The <see cref="ISyncRepository{T}"/> repository.</typeparam>
    public interface ISyncRepository<T> where T : class
    {
        /// <summary>
        /// Gets all entry from the repository.
        /// </summary>
        /// <returns>The all entry from the repository.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets the <see cref="T"/> entry from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="T"/> entry.</param>
        /// <returns>The <see cref="T"/> entry with the given ID.</returns>
        T Get(int id);

        /// <summary>
        /// Finds a <see cref="T"/> entry from repository.
        /// </summary>
        /// <param name="predicate">Logic predicate <see cref="Func{T, TResult}"/> that defines a set 
        /// of criteria and determines whether the specified object meets those criteria.</param>
        /// <returns>Collection of <see cref="T"/> entry.</returns>
        IEnumerable<T> Find(Func<T, bool> predicate);

        /// <summary>
        /// Finds a first entry from repository.
        /// </summary>
        /// <param name="predicate">Logic predicate <see cref="Func{T, TResult}"/> that defines a set 
        /// of criteria and determines whether the specified object meets those criteria.</param>
        /// <returns>The <see cref="T"/> entry with the given predicate.</returns>
        T FindFirst(Func<T, bool> predicate);
    }
}