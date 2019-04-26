using Domain.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    /// <summary>
    /// Represents a merged user entity. 
    /// </summary>
    public class IdentityDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        [Required]
        public string GUID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the midname.
        /// </summary>
        public string Midname { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        [Required]
        public bool? Gender { get; set; }

        /// <summary>
        /// Gets or sets the birthdate.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Gets or sets the picture.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the city or village.
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [Phone]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        [Required]
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        [Required]
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the user status.
        /// </summary>
        [Required]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        [Required]
        public string Role { get; set; }

        /// <summary>
        /// Initializes a <see cref="IdentityDTO"/> class.
        /// </summary>
        public IdentityDTO() { }

        /// <summary>
        /// Initializes a <see cref="IdentityDTO"/> class after cast from <see cref="Identity"/> object.
        /// </summary>
        public IdentityDTO(Identity item)
        {
            ID = item.ID;
            Picture = item.Picture;
            GUID = item.GUID;
        }

        /// <summary>
        /// Initializes a <see cref="IdentityDTO"/> class after concatenates <see cref="Identity"/> 
        /// object and <see cref="User"/> object.
        /// </summary>
        public IdentityDTO(Identity identity, User user)
        {
            ID = identity.ID;
            GUID = identity.GUID;
            Picture = identity.Picture;
            Name = user.Name;
            Surname = user.Surname;
            Midname = user.Midname;
            Birthdate = user.Birthdate;
            Gender = user.Gender;
            Country = user.Country;
            City = user.City;
            Phone = user.Phone;
            Email = user.Email;
            Department = user.Department;
            Group = user.Group;
            Status = user.Status;
            Role = user.Role;
        }

        /// <summary>
        /// Overrides default type conversion operator to a user-defined type conversion operator 
        /// that must be invoked with a cast.
        /// </summary>
        /// <param name="item">The <see cref="Identity"/> object</param>
        public static explicit operator IdentityDTO(Identity item)
        {
            return new IdentityDTO(item);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is IdentityDTO item)
            {
                result = ID == item.ID;
                result &= GUID.Equals(item.GUID);
                result &= Name.Equals(item.Name);
                result &= Surname.Equals(item.Surname);
                result &= Midname.Equals(item.Midname);
                result &= Birthdate.Equals(item.Birthdate);
                result &= Gender.Equals(item.Gender);
                result &= Country.Equals(item.Country);
                result &= City.Equals(item.City);
                result &= Phone.Equals(item.Phone);
                result &= Email.Equals(item.Email);
                result &= Department.Equals(item.Department);
                result &= Group.Equals(item.Group);
                result &= Status.Equals(item.Status);
                result &= Role.Equals(item.Role);
                return result;
            }
            return false;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= GUID.GetHashCode();
            hashcode ^= Picture.GetHashCode();
            hashcode ^= Name.GetHashCode();
            hashcode ^= Surname.GetHashCode();
            hashcode ^= Midname.GetHashCode();
            hashcode ^= Birthdate.GetHashCode();
            hashcode ^= Gender.GetHashCode();
            hashcode ^= Country.GetHashCode();
            hashcode ^= City.GetHashCode();
            hashcode ^= Phone.GetHashCode();
            hashcode ^= Email.GetHashCode();
            hashcode ^= Department.GetHashCode();
            hashcode ^= Group.GetHashCode();
            hashcode ^= Status.GetHashCode();
            hashcode ^= Role.GetHashCode();
            return hashcode;
        }
    }
}