using Domain.Core;
using Service.DTO;
using System.Collections.Generic;
using System.Web;

namespace Service.Interfaces
{
    /// <summary>
    /// Represents a facade pattern and business logic.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Creates the <see cref="Identity"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Identity"/> object.</param>
        void Create(Identity model);

        /// <summary>
        /// Deletes the <see cref="Identity"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Identity"/> object.</param>
        void Delete(Identity model);

        /// <summary>
        /// Performs <see cref="UnitOfWork.Dispose()"/>.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Edits the <see cref="Identity"/> object in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Identity"/> object.</param>
        void Edit(Identity model);

        /// <summary>
        /// Finds a <see cref="IdentityDTO"/> in the repository.
        /// </summary>
        /// <param name="model">The <see cref="Identity"/> object.</param>
        /// <returns>The <see cref="IdentityDTO"/> object.</returns>
        IdentityDTO Find(Identity model);

        /// <summary>
        /// Gets a <see cref="IdentityDTO"/> object from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="IdentityDTO"/>.</param>
        /// <returns>The <see cref="IdentityDTO"/> with the given ID.</returns>
        IdentityDTO Get(int? id);

        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="IdentityDTO"/> from the repository.</returns>
        IEnumerable<IdentityDTO> GetAll();

        /// <summary>
        /// Gets a <see cref="IdentityDTO"/> from repository by department.
        /// </summary>
        /// <param name="department">The identity department.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> from the repository which equals the 
        /// <paramref name="department"/>.</returns>
        IEnumerable<IdentityDTO> GetByDepartment(string department);

        /// <summary>
        /// Gets a <see cref="IdentityDTO"/> from repository by group.
        /// </summary>
        /// <param name="group">The user group.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> from the repository which equals the 
        /// <paramref name="group"/>.</returns>
        IEnumerable<IdentityDTO> GetByGroup(string group);

        /// <summary>
        /// Gets a <see cref="IdentityDTO"/> from repository by GUID.
        /// </summary>
        /// <param name="group">The user GUID.</param>
        /// <returns>The <see cref="IdentityDTO"/> with the given GUID.</returns>
        IdentityDTO GetByGUID(string guid);

        /// <summary>
        /// Gets a <see cref="IdentityDTO"/> from repository by full name.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <param name="midname">The user midname.</param>
        /// <param name="surname">The user surname.</param>
        /// <returns>The <see cref="IdentityDTO"/> with the given full name.</returns>
        IdentityDTO GetByName(string name, string midname, string surname);

        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository by status.
        /// </summary>
        /// <param name="status">The user status.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> from the repository which equals the 
        /// <paramref name="status"/>.</returns>
        IEnumerable<IdentityDTO> GetByStatus(string status);

        /// <summary>
        /// Gets DTO-model of Identity.
        /// </summary>
        /// <param name="guid">The identity GUID.</param>
        /// <returns>The <see cref="IdentityDTO"/> with the given ID.</returns>
        IdentityDTO GetFull(string guid);

        /// <summary>
        /// Gets a <see cref="Identity"/> from repository.
        /// </summary>
        /// <param name="id">The identity ID.</param>
        /// <returns>The <see cref="Identity"/> with the given ID.</returns>
        Identity GetSimple(int? id);

        /// <summary>
        /// Gets User from Active Directory.
        /// </summary>
        /// <param name="identityValue">The identity value. <example>"Петр Петров", 
        /// "ivanov_ivan", etc</example>.</param>
        /// <param name="domain">The domain.<example>"aptech.com", "sstu.com", etc. 
        /// Can be used Environment.UserDomainName</example>.</param>
        UserDTO GetUser(string identityValue, string domain);

        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from the 1C-repository.
        /// </summary>
        /// <returns>The collection of all <see cref="User"/> from the 1C-repository.</returns>
        IEnumerable<User> GetUsers1C();

        /// <summary>
        /// Saves <see cref="HttpPostedFileBase"/> to ~/Content/uploads/.
        /// </summary>
        /// <param name="data">The picture stream.</param>
        /// <returns>The path of saved picture.</returns>
        string SaveImage(HttpPostedFileBase data);

        /// <summary>
        /// Returns the boolean result of finding user in Active Directory by 
        /// <paramref name="identityValue"/>.
        /// </summary>
        /// <param name="identityValue"><example>"Петр Петров", "ivanov_ivan", 
        /// etc</example>.</param>
        /// <param name="domain"><example>"aptech.com", "sstu.com", etc. Can be 
        /// used Environment.UserDomainName</example>.</param>
        bool IsUserExist(string identityValue, string domain);

        /// <summary>
        /// Returns the boolean result of validated <paramref name="username"/> 
        /// and <paramref name="password"/> in Active Directory.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The user password.</param>
        /// <param name="domain"><example>"aptech.com", "sstu.com", etc. Can be 
        /// used Environment.UserDomainName</example>.</param>
        bool IsValidUser(string username, string password, string domain);
    }
}