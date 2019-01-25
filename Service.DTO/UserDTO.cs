using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace Service.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Roles { get; set; }

        public UserDTO() { }

        public UserDTO(UserPrincipal item)
        {
            Name = item.GivenName;
            Surname = item.Surname;
            Roles = item.GetGroups().Select(x => x.Name).ToList();
        }

        public static explicit operator UserDTO(UserPrincipal item)
        {
            return new UserDTO(item);
        }
    }
}