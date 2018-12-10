using System;

namespace Domain.Core
{
    public class User
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Midname { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Group { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
    }
}