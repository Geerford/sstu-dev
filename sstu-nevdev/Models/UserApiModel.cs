using Service.DTO;
using System.Collections.Generic;

namespace sstu_nevdev.Models
{
    /// <summary>
    /// Additional information about User from ActiveDirectory
    /// </summary>
    public class UserApiModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Roles { get; set; }

        public UserApiModel() { }

        public UserApiModel(UserDTO item)
        {
            Name = item.Name;
            Surname = item.Surname;
            Roles = item.Roles;
        }

        public static explicit operator UserApiModel(UserDTO item)
        {
            return new UserApiModel(item);
        }
    }
}