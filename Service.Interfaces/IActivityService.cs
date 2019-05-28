using Domain.Core;
using Service.DTO;
using System.Collections.Generic;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface IActivityService
    {
        /// <summary>
        /// Creates the <see cref="Activity"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Activity"/> object.</param>
        void Create(Activity model);

        /// <summary>
        /// Deletes the <see cref="Activity"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Activity"/> object.</param>
        void Delete(Activity model);

        /// <summary>
        /// Performs <see cref="UnitOfWork.Dispose()"/>.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Edits the <see cref="Activity"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Activity"/> object.</param>
        void Edit(Activity model);

        /// <summary>
        /// Gets a <see cref="Activity"/> object from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="Activity"/>.</param>
        /// <returns>The <see cref="Activity"/> with the given ID.</returns>
        Activity Get(int? id);

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="Activity"/> from the repository.</returns>
        IEnumerable<Activity> GetAll();

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by status.
        /// </summary>
        /// <param name="status">True if the <see cref="Activity"/> is successful; otherwise,false</param>
        /// <returns>The collection of all <see cref="Activity"/> from the repository which equals the 
        /// <paramref name="status"/>.</returns>
        IEnumerable<Activity> GetByStatus(bool? status);

        /// <summary>
        /// Checks admissions of the user.
        /// </summary>
        /// <param name="checkpointID">The checkpoint ID.</param>
        /// <param name="role">The user role.</param>
        bool IsAdmission(int? checkpointID, string role);

        /// <summary>
        /// Adds new activity and returns one of codes: 
        /// <para>-1 -- Ok. No response.</para> 
        /// <para>200 -- Ok. Return response.</para> 
        /// <para>500 -- Fail. Permission denied.</para>
        /// </summary>
        /// <param name="checkpoint">The <see cref="CheckpointDTO"/>.</param>
        /// <param name="identity">The <see cref="IdentityDTO"/>.</param>
        int IsOk(CheckpointDTO checkpoint, IdentityDTO identity);

        /// <summary>
        /// True if the user is at university; otherwise, false.
        /// </summary>
        /// <param name="identityGUID">The user GUID.</param>
        bool IsPassed(string identityGUID);

        /// <summary>
        /// True if the user is in classroom; otherwise, false.
        /// </summary>
        /// <param name="identityGUID">The user GUID.</param>
        bool IsInClassroom(string identityGUID);
    }
}