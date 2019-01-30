using Domain.Core;
using System.Collections.Generic;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface IAdmissionService
    {
        /// <summary>
        /// Creates the <see cref="Admission"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Admission"/> object.</param>
        void Create(Admission model);

        /// <summary>
        /// Deletes the <see cref="Admission"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Admission"/> object.</param>
        void Delete(Admission model);

        /// <summary>
        /// Performs <see cref="UnitOfWork.Dispose()"/>.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Edits the <see cref="Admission"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Admission"/> object.</param>
        void Edit(Admission model);

        /// <summary>
        /// Gets a <see cref="Admission"/> object from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="Admission"/>.</param>
        /// <returns>The <see cref="Admission"/> with the given ID.</returns>
        Admission Get(int? id);

        /// <summary>
        /// Gets all <see cref="Admission"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="Admission"/> from the repository.</returns>
        IEnumerable<Admission> GetAll();
    }
}