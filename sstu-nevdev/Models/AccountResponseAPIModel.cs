using Service.DTO;
using System.Collections.Generic;

namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents additional information about User from ActiveDirectory for response.
    /// </summary>
    public class AccountResponseAPIModel
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
        /// Gets or sets the roles collection.
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// Gets or sets the grasshopper key.
        /// </summary>
        public byte[] GrasshopperKey { get; set; }

        /// <summary>
        /// Initializes a <see cref="AccountResponseAPIModel"/> class.
        /// </summary>
        public AccountResponseAPIModel() { }

        /// <summary>
        /// Initializes a <see cref="AccountResponseAPIModel"/> class after cast from <see cref="UserDTO"/> object.
        /// </summary>
        public AccountResponseAPIModel(UserDTO item)
        {
            Name = item.Name;
            Surname = item.Surname;
            Roles = item.Roles;
        }

        /// <summary>
        /// Overrides default type conversion operator to a user-defined type conversion operator 
        /// that must be invoked with a cast.
        /// </summary>
        /// <param name="item">The <see cref="UserDTO"/> object</param>
        public static explicit operator AccountResponseAPIModel(UserDTO item)
        {
            return new AccountResponseAPIModel(item);
        }
    }
}