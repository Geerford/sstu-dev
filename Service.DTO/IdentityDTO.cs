using Domain.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    public class IdentityDTO
    {
        public int ID { get; set; }
        [Required]
        public string GUID { get; set; }
        public string RFID { get; set; }
        public string QR { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Midname { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }
        public string Picture { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Group { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Role { get; set; }

        public IdentityDTO() { }

        public IdentityDTO(Identity item)
        {
            ID = item.ID;
            Picture = item.Picture;
            GUID = item.GUID;
        }

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

        public static explicit operator IdentityDTO(Identity item)
        {
            return new IdentityDTO(item);
        }

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