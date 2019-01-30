using Domain.Core;
using System.Collections.Generic;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface ITypeService
    {
        /// <summary>
        /// Creates the <see cref="Type"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Type"/> object.</param>
        void Create(Type model);

        /// <summary>
        /// Deletes the <see cref="Type"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Type"/> object.</param>
        void Delete(Type model);

        /// <summary>
        /// Performs <see cref="UnitOfWork.Dispose()"/>.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Edits the <see cref="Type"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Type"/> object.</param>
        void Edit(Type model);

        /// <summary>
        /// Gets a <see cref="Type"/> object from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="Type"/>.</param>
        /// <returns>The <see cref="Type"/> with the given ID.</returns>
        Type Get(int? id);

        /// <summary>
        /// Gets all <see cref="Type"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="Type"/> from the repository.</returns>
        IEnumerable<Type> GetAll();

        /// <summary>
        /// Gets all <see cref="Type"/> from repository by status.
        /// </summary>
        /// <param name="status">True if the <see cref="Type"/> is successful; otherwise,false</param>
        /// <returns>The collection of <see cref="Type"/> from the repository which equals the 
        /// <paramref name="status"/>.</returns>
        IEnumerable<Type> GetByStatus(string status);
    }
}