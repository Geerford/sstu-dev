using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    public class Identity
    {
        public int ID { get; set; }
        public string RFID { get; set; }
        public string QR { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Surname { get; set; }
        [StringLength(20)]
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
        [StringLength(30)]
        public string Department { get; set; }
        [Required]
        [StringLength(30)]
        public string Group { get; set; }
        [Required]
        [StringLength(20)]
        public string Status { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; } 

        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
