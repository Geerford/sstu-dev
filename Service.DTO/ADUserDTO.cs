using System.Collections.Generic;

namespace Service.DTO
{
    /// <summary>
    /// Represents a active directory user entity. 
    /// </summary>
    public class ADUserDTO
    {
        /// <summary>
        /// Gets or sets the user logon name.
        /// </summary>
        public string SamAccountName { get; set; }

        /// <summary>
        /// Gets or sets the user distinguished name.
        /// </summary>
        public string DN { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public IEnumerable<string> Roles { get; set; }
    }
}