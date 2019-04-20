using Domain.Core;
using System;
using System.Collections.Generic;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface IModeService
    {
        /// <summary>
        /// Creates the <see cref="Mode"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Mode"/> object.</param>
        void Create(Mode model);

        /// <summary>
        /// Deletes the <see cref="Mode"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Mode"/> object.</param>
        void Delete(Mode model);

        /// <summary>
        /// Performs <see cref="UnitOfWork.Dispose()"/>.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Edits the <see cref="Mode"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Mode"/> object.</param>
        void Edit(Mode model);

        /// <summary>
        /// Gets a <see cref="Mode"/> object from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="Mode"/>.</param>
        /// <returns>The <see cref="Mode"/> with the given ID.</returns>
        Mode Get(int? id);

        /// <summary>
        /// Gets all <see cref="Mode"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="Mode"/> from the repository.</returns>
        IEnumerable<Mode> GetAll();

        /// <summary>
        /// Gets all <see cref="Mode"/> from repository by status.
        /// </summary>
        /// <param name="mode">True if the <see cref="Mode"/> is successful; otherwise,false</param>
        /// <returns>The collection of <see cref="Mode"/> from the repository which equals the 
        /// <paramref name="mode"/>.</returns>
        IEnumerable<Mode> GetByMode(string mode);
    }
}