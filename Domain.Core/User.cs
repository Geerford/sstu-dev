using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Core
{
    /// <summary>
    /// Represents a user entity from enterprise database. 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        [Index(IsUnique = true)]
        public string GUID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the midname.
        /// </summary>
        public string Midname { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// Gets or sets the birthdate.
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the user status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        public string Role { get; set; }
    }
}