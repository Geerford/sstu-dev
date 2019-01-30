using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace Service.DTO
{
    /// <summary>
    /// Represents a user entity from Active Directory to response.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the collection of roles.
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// Initializes a <see cref="UserDTO"/> class.
        /// </summary>
        public UserDTO() { }

        /// <summary>
        /// Initializes a <see cref="UserDTO"/> class after cast from <see cref="UserPrincipal"/> object.
        /// </summary>
        public UserDTO(UserPrincipal item)
        {
            Name = item.GivenName;
            Surname = item.Surname;
            Roles = item.GetGroups().Select(x => x.Name).ToList();
        }

        /// <summary>
        /// Overrides default type conversion operator to a user-defined type conversion operator 
        /// that must be invoked with a cast.
        /// </summary>
        /// <param name="item">The <see cref="UserPrincipal"/> object</param>
        public static explicit operator UserDTO(UserPrincipal item)
        {
            return new UserDTO(item);
        }
    }
}