using Domain.Core;
using Service.DTO;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Web;

namespace Service.Interfaces
{
    public interface IIdentityService
    {
        Identity GetSimple(int? id);
        IdentityDTO Get(int? id);
        IdentityDTO GetByGUID(string guid);
        IdentityDTO GetByName(string name, string midname, string surname);
        IEnumerable<IdentityDTO> GetAll();
        IEnumerable<IdentityDTO> GetByStatus(string status);
        IEnumerable<IdentityDTO> GetByDepartment(string department);
        IEnumerable<IdentityDTO> GetByGroup(string group);
        IdentityDTO GetFull(string guid);
        void Create(Identity model);
        void Edit(Identity model);
        void Delete(Identity model);
        IdentityDTO Find(Identity model);
        string SaveImage(HttpPostedFileBase data);

        bool IsUserExist(string identityValue, string domain);
        bool IsValidUser(string user, string password, string domain);
        UserPrincipal GetUser(string identityValue, string domain);

        IEnumerable<User> GetUsers1C();

        void Dispose();
    }
}