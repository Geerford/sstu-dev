using Domain.Core;
using Microsoft.Owin.Security;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Security.Claims;
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

        public void Create(Identity model)
        {
            User user = SyncDatabase.User.GetAll().Where(x => x.GUID.Equals(model.GUID)).FirstOrDefault();
            if (user == null)
            {
                throw new ValidationException("Сущность не найдена в синхронизируемой БД", "");
            }
            Identity identity = Database.Identity.GetAll().Where(x => x.GUID.Equals(model.GUID)).FirstOrDefault();
            if (identity != null)
            {
                throw new ValidationException("Сущность для данного пользователя уже существует", "");
            }
            Database.Identity.Create(model);
            Database.Save();
        }

        public void Delete(Identity model)
        {
            Database.Identity.Delete(model.ID);
            Database.Save();
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

        public IdentityDTO Find(Identity model)
        {
            return GetFull(model.GUID);
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

        public IdentityDTO GetByName(string name, string midname, string surname)
        {
            User item = SyncDatabase.User.Find(x => (x.Surname == surname) && (x.Name == name) 
                && (x.Midname == midname)).FirstOrDefault();
            return GetFull(item.GUID);
        }

        public IEnumerable<IdentityDTO> GetAll()
        {
            List<IdentityDTO> result = new List<IdentityDTO>();
            foreach (var item in Database.Identity.GetAll())
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

        /// <summary>
        /// Get DTO-model of Identity
        /// </summary>
        /// <param name="guid">GUID of Identity</param>
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

        /// <summary>
        /// Save HttpPosedFileBase to ~/Content/uploads/
        /// </summary>
        public string SaveImage(HttpPostedFileBase data)
        {
            string pathName = "";
            if (data != null)
            {
                string extension = Path.GetExtension(data.FileName),
                    path = HttpContext.Current.Server.MapPath("~/Content/uploads/");
                pathName = Guid.NewGuid().ToString() + extension;
                data.SaveAs(path + pathName);
            }
            return pathName;
        }

        /// <param name="identityValue">Examples: "Петр Петров", "ivanov_ivan", etc</param>
        /// <param name="domain">Examples: "aptech.com", "sstu.com", etc. Can be used Environment.UserDomainName</param>
        public bool IsUserExist(string identityValue, string domain)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, identityValue);
                return user != null ? true : false;
            }
        }

        /// <param name="domain">Examples: "aptech.com", "sstu.com", etc. Can be used Environment.UserDomainName</param>
        public bool IsValidUser(string user, string password, string domain)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                return context.ValidateCredentials(user.Trim(), password.Trim());
            }
        }

        /// <param name="identityValue">Examples: "Петр Петров", "ivanov_ivan", etc</param>
        /// /// <param name="domain">Examples: "aptech.com", "sstu.com", etc. Can be used Environment.UserDomainName</param>
        public UserDTO GetUser(string identityValue, string domain)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, identityValue);
                if(user != null)
                {
                    return (UserDTO)user;
                }
                return null;
            }
        }

        public class Authentication
        {
            private readonly IAuthenticationManager authenticationManager;

            public Authentication(IAuthenticationManager authenticationManager)
            {
                this.authenticationManager = authenticationManager;
            }

            /// <summary>
            /// Type of custom authentication for pattern-work
            /// </summary>
            public static class SSTUAuthentication
            {
                public const string ApplicationCookie = "SSTUAuthenticationType";
            }

            /// <summary>
            /// Authentication is successful if there are no ErrorMessage
            /// </summary>
            public class AuthenticationResult
            {
                public string ErrorMessage { get; private set; }
                public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);

                public AuthenticationResult(string ErrorMessage = null)
                {
                    this.ErrorMessage = ErrorMessage;
                }
            }

            /// <summary>
            /// Check if username and password matches existing account in ActiveDirectory. 
            /// </summary>
            public AuthenticationResult SignIn(string username, string password)
            {
                var parsedName = ParseUsername(username);
                string domain, name;
                if (parsedName != null)
                {
                    domain = parsedName[0];
                    name = parsedName[1];
                }
                else
                {
                    domain = Environment.UserDomainName;
                    name = username;
                }
                using (var сontext = new PrincipalContext(ContextType.Domain, domain))
                {
                    UserPrincipal user = null;
                    bool isAuthenticated = false;
                    try
                    {
                        user = UserPrincipal.FindByIdentity(сontext, name);
                        if (user != null)
                        {
                            isAuthenticated = сontext.ValidateCredentials(name, password, ContextOptions.Negotiate);
                        }
                    }
                    catch (Exception)
                    {
                        return new AuthenticationResult("Неверно введен логин или пароль.");
                    }
                    if (!isAuthenticated)
                    {
                        return new AuthenticationResult("Неверно введен логин или пароль.");
                    }
                    if (user.IsAccountLockedOut())
                    {
                        return new AuthenticationResult("Ваша учетная запись заблокирована администратором.");
                    }
                    if (user.Enabled.HasValue && user.Enabled.Value == false)
                    {
                        return new AuthenticationResult("Ваша учетная запись отключена.");
                    }
                    var identity = CreateIdentity(user);

                    authenticationManager.SignOut(SSTUAuthentication.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
                    return new AuthenticationResult();
                }
            }

            /// <summary>
            /// Parse username and domain from string
            /// </summary>
            /// <returns>string[0] -- Domain name; string[1] -- Username</returns>
            string[] ParseUsername(string username)
            {
                if (username.Contains('\\'))
                {
                    var usernameParsed = username.Split('\\');
                    return new string[] { usernameParsed[0].ToString(), usernameParsed[1] };
                }

                if (username.Contains('@'))
                {
                    var logonNameParts = username.Split('@');
                    return new string[] { logonNameParts[1].ToString(), logonNameParts[0] };
                }
                return null;
            }

            /// <summary>
            /// Create new ClaimsIdentity to save it in cookie
            /// </summary>
            /// <param name="userPrincipal">User from ActiveDirectory</param>
            private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
            {
                var identity = new ClaimsIdentity(SSTUAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
                identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
                if (!string.IsNullOrEmpty(userPrincipal.EmailAddress))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
                }
                foreach (var item in userPrincipal.GetGroups())
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, item.Name));
                }
                return identity;
            }
        }

        public IEnumerable<User> GetUsers1C()
        {
            return SyncDatabase.User.GetAll();
        }
    }
}