using Domain.Core;
using Service.DTO;
using System.Collections.Generic;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface ICheckpointService
    {
        /// <summary>
        /// Creates the <see cref="Checkpoint"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="CheckpointDTO"/> object.</param>
        void Create(CheckpointDTO model);

        /// <summary>
        /// Deletes the <see cref="Checkpoint"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Checkpoint"/> object.</param>
        void Delete(Checkpoint model);

        /// <summary>
        /// Deletes the <see cref="CheckpointDTO"/> object in the repository with given checkpoint 
        /// ID and admission ID.
        /// </summary>
        /// <param name="checkpointID">The checkpoint ID.</param>
        /// <param name="admissionID">The admission ID.</param>
        void Delete(int? checkpointID, int? admissionID);

        /// <summary>
        /// Deletes all admissions for checkpoint with given ID.
        /// </summary>
        /// <param name="checkpointID">The checkpoint ID.</param>
        void DeleteAllAdmission(int? checkpointID);

        /// <summary>
        /// Performs <see cref="UnitOfWork.Dispose()"/>.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Edits the <see cref="CheckpointDTO"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="CheckpointDTO"/> object.</param>
        void Edit(CheckpointDTO model);

        /// <summary>
        /// Gets a <see cref="CheckpointDTO"/> object from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="CheckpointDTO"/>.</param>
        /// <returns>The <see cref="CheckpointDTO"/> with the given ID.</returns>
        CheckpointDTO Get(int? id);

        /// <summary>
        /// Gets all <see cref="CheckpointDTO"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="CheckpointDTO"/> from the repository.</returns>
        IEnumerable<CheckpointDTO> GetAll();

        /// <summary>
        /// Gets a <see cref="CheckpointDTO"/> from repository by IP-address.
        /// </summary>
        /// <param name="ip">The checkpoint IP-address.</param>
        /// <returns>The <see cref="CheckpointDTO"/> with the given IP-address.</returns>
        CheckpointDTO GetByIP(string ip);

        /// <summary>
        /// Gets all <see cref="CheckpointDTO"/> from repository by status.
        /// </summary>
        /// <param name="status">The checkpoint status.</param>
        /// <returns>The collection of all <see cref="CheckpointDTO"/> from the repository which equals the 
        /// <paramref name="status"/>.</returns>
        IEnumerable<CheckpointDTO> GetByStatus(string status);

        /// <summary>
        /// Gets all <see cref="CheckpointDTO"/> from repository by type.
        /// </summary>
        /// <param name="type">The checkpoint type.</param>
        /// <returns>The collection of all <see cref="CheckpointDTO"/> from the repository which equals the 
        /// <paramref name="type"/>.</returns>
        IEnumerable<CheckpointDTO> GetByType(string type);

        /// <summary>
        /// Gets DTO-model of Checkpoint.
        /// </summary>
        /// <param name="id">The checkpoint ID.</param>
        CheckpointDTO GetFull(int? id);

        /// <summary>
        /// Gets a <see cref="Checkpoint"/> from repository.
        /// </summary>
        /// <param name="id">The checkpoint ID.</param>
        /// <returns>The <see cref="Checkpoint"/> with the given ID.</returns>
        Checkpoint GetSimple(int? id);
       
        /// <summary>
        /// Checks existence of admission.
        /// </summary>
        /// <param name="checkpointID">ID of Checkpoint.</param>
        /// <param name="admissionID">ID of Admission.</param>
        bool IsMatchAdmission(int checkpointID, int admissionID);
    }
}