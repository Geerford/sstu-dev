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
            RFID = item.RFID;
            QR = item.QR;
            Picture = item.Picture;
            GUID = item.GUID;
        }

        public IdentityDTO(Identity identity, User user)
        {
            ID = identity.ID;
            GUID = identity.GUID;
            RFID = identity.RFID;
            QR = identity.QR;
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
    }
}
