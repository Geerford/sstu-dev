using Domain.Core;
using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface IStatisticsService
    {
        /// <summary>
        /// Performs <see cref="UnitOfWork.Dispose()"/>.
        /// </summary>
        void Dispose();

        IEnumerable<IdentityDTO> GetStatistics(int? campus, int? row, int? classroom, string name, string midname, string surname, DateTime? start, DateTime? end);

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by campus and time interval [start, end].
        /// </summary>
        /// <param name="campus">The campus.</param>
        /// <param name="start">The left endpoint of time interval.</param>
        /// <param name="end">The right endpoint of time interval.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given campus and time interval [start, end].</returns>
        IEnumerable<ActivityDTO> GetByCampus(int campus, DateTime start, DateTime end);

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by campus, row and time interval [start, end].
        /// </summary>
        /// <param name="campus">The campus.</param>
        /// <param name="row">The row.</param>
        /// <param name="start">The left endpoint of time interval.</param>
        /// <param name="end">The right endpoint of time interval.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given campus, row and time interval [start, end].</returns>
        IEnumerable<ActivityDTO> GetByCampusRow(int campus, int row, DateTime start, DateTime end);

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by classroom and time interval [start, end].
        /// </summary>
        /// <param name="classroom">The classroom.</param>
        /// <param name="start">The left endpoint of time interval.</param>
        /// <param name="end">The right endpoint of time interval.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given classroom and time interval [start, end].</returns>
        IEnumerable<ActivityDTO> GetByClassroom(int classroom, DateTime start, DateTime end);

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by group and time interval [start, end].
        /// </summary>
        /// <param name="group">The user group.</param>
        /// <param name="start">The left endpoint of time interval.</param>
        /// <param name="end">The right endpoint of time interval.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given group and time interval [start, end].</returns>
        IEnumerable<ActivityDTO> GetByGroup(string group, DateTime start, DateTime end);

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by section and time interval [start, end].
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="start">The left endpoint of time interval.</param>
        /// <param name="end">The right endpoint of time interval.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given section and time interval [start, end].</returns>
        IEnumerable<ActivityDTO> GetBySection(int section, DateTime start, DateTime end);

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by full name and time interval [start, end].
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <param name="midname">The user midname.</param>
        /// <param name="surname">The user surname.</param>
        /// <param name="start">The left endpoint of time interval.</param>
        /// <param name="end">The right endpoint of time interval.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given full name and time interval [start, end].</returns>
        IEnumerable<ActivityDTO> GetByUser(string name, string midname, string surname, DateTime start, DateTime end);

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by GUID and time interval [start, end].
        /// </summary>
        /// <param name="guid">The user GUID.</param>
        /// <param name="start">The left endpoint of time interval.</param>
        /// <param name="end">The right endpoint of time interval.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given full name and time interval [start, end].</returns>
        IEnumerable<ActivityDTO> GetByUser(string guid, DateTime start, DateTime end);

        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository by campus.
        /// </summary>
        /// <param name="campus">The campus.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> with the given campus.</returns>
        IEnumerable<IdentityDTO> GetCurrentByCampus(int campus);

        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository by campus and row.
        /// </summary>
        /// <param name="campus">The campus.</param>
        /// <param name="row">The row.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> with the given campus and row.</returns>
        IEnumerable<IdentityDTO> GetCurrentByCampusRow(int campus, int row);

        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository by classroom.
        /// </summary>
        /// <param name="classroom">The classroom.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> with the given classroom.</returns>
        IEnumerable<IdentityDTO> GetCurrentByClassroom(int classroom);

        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository by section.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> with the given section.</returns>
        IEnumerable<IdentityDTO> GetCurrentBySection(int section);

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by full name.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <param name="midname">The user midname.</param>
        /// <param name="surname">The user surname.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given full name.</returns>
        ActivityDTO GetUserLocation(string name, string midname, string surname);

        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by GUID.
        /// </summary>
        /// <param name="guid">The user GUID.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given full name.</returns>
        ActivityDTO GetUserLocation(string guid); 

        /// <summary>
        /// Gets all <see cref="string"/> roles from active directory repository.
        /// </summary>
        /// <param name="domain">The active directory domain.</param>
        /// <returns>The collection of all <see cref="string"/> roles.</returns>
        IEnumerable<string> GetRoles(string domain);

        /// <summary>
        /// Gets all <see cref="ADUserDTO"/> from active directory repository.
        /// </summary>
        /// <param name="domain">The active directory domain.</param>
        /// <returns>The collection of all <see cref="ADUserDTO"/>.</returns>
        IEnumerable<ADUserDTO> GetUsers(string domain);

        /// <summary>
        /// Gets all <see cref="ADUserDTO"/> from repository by role.
        /// </summary>
        /// <param name="domain">The active directory domain.</param>
        /// <param name="role">The role.</param>
        /// <returns>The collection of all <see cref="ADUserDTO"/> with the given.</returns>
        IEnumerable<ADUserDTO> GetUsersByRole(string domain, string role);
    }
}