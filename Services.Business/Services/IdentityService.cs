using Domain.Core;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Web;

namespace Services.Business.Services
{
    public class IdentityService : IIdentityService
    {
        IUnitOfWork Database { get; set; }
        ISyncUnitOfWork SyncDatabase { get; set; }

        public IdentityService(IUnitOfWork uow, ISyncUnitOfWork suow)
        {
            Database = uow;
            SyncDatabase = suow;
        }

        public Identity GetSimple(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Identity item = Database.Identity.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IdentityDTO Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Identity item = Database.Identity.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return GetFull(item.GUID);
        }  

        public IdentityDTO GetByGUID(string guid)
        {
            return GetFull(guid);
        }

        public IdentityDTO GetByRFID(string rfid)
        {
            Identity item = Database.Identity.Find(x => x.RFID == rfid).FirstOrDefault();
            return GetFull(item.GUID);
        }

        public IdentityDTO GetByQR(string qr)
        {
            Identity item = Database.Identity.Find(x => x.QR == qr).FirstOrDefault();
            return GetFull(item.GUID);
        }

        public IdentityDTO GetByName(string name, string midname, string surname)
        {
            User item = SyncDatabase.User.Find(x => (x.Surname == surname) && (x.Name == name) && (x.Midname == midname)).FirstOrDefault();
            return GetFull(item.GUID);
        }

        public IEnumerable<IdentityDTO> GetAll()
        {
            List<IdentityDTO> result = new List<IdentityDTO>();
            foreach(var item in Database.Identity.GetAll())
            {
                result.Add(GetFull(item.GUID));
            }
            return result;
        }

        public IEnumerable<IdentityDTO> GetByStatus(string status)
        {
            List<IdentityDTO> result = new List<IdentityDTO>();
            foreach (var item in SyncDatabase.User.GetAll().Where(x => x.Status == status))
            {
                result.Add(GetFull(item.GUID));
            }
            return result;
        }

        public IEnumerable<IdentityDTO> GetByDepartment(string department)
        {
            List<IdentityDTO> result = new List<IdentityDTO>();
            foreach (var item in SyncDatabase.User.GetAll().Where(x => x.Department == department))
            {
                result.Add(GetFull(item.GUID));
            }
            return result;
        }

        public IEnumerable<IdentityDTO> GetByGroup(string group)
        {
            List<IdentityDTO> result = new List<IdentityDTO>();
            foreach (var item in SyncDatabase.User.GetAll().Where(x => x.Group == group))
            {
                result.Add(GetFull(item.GUID));
            }
            return result;
        }

        public IdentityDTO GetFull(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                throw new ValidationException("Не задан ID", "");
            }
            Identity identity = Database.Identity.GetAll().Where(x => x.GUID.Equals(guid)).FirstOrDefault();
            if (identity == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            User user = SyncDatabase.User.GetAll().Where(x => x.GUID.Equals(guid)).FirstOrDefault();
            if (user == null)
            {
                throw new ValidationException("Сущность не найдена в синхронизируемой БД", "");
            }
            return new IdentityDTO(identity, user);
        }

        public void Create(Identity model)
        {
            Database.Identity.Create(model);
            Database.Save();
        }

        public void Delete(Identity model)
        {
            Database.Identity.Delete(model.ID);
            Database.Save();
        }

        public IdentityDTO Find(Identity model)
        {
            return GetFull(model.GUID);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Identity model)
        {
            Database.Identity.Update(model);
            Database.Save();
        }

        public string SaveImage(HttpPostedFileBase data)
        {
            string path = HttpContext.Current.Server.MapPath("~/Content/uploads/"), pathName = "";
            if (data != null)
            {
                string extension = Path.GetExtension(data.FileName);
                pathName = System.Guid.NewGuid().ToString() + extension;
                data.SaveAs(path + pathName);
            }
            return pathName;
        }

        ///identityValue : "Петр Петров", "ivanov_ivan"...
        ///domain : "aptech.com"
        public bool IsUserExist(string identityValue, string domain)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, identityValue);
                return user != null ? true : false;
            }
        }

        public bool IsValidUser(string user, string password)
        {
            using (var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
            {
                return context.ValidateCredentials(user.Trim(), password.Trim());
            }
        }

        public UserPrincipal GetUser(string identityValue, string domain)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, identityValue);
                return user ?? null;
            }
        }
    }
}