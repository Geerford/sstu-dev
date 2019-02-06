using Domain.Core;
using Microsoft.Owin.Security;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using Services.Business.Security;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Services.Business.Service
{
    /// <summary>
    /// Implements <see cref="IIdentityService"/>.
    /// </summary>
    public class IdentityService : IIdentityService
    {
        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        ISyncUnitOfWork SyncDatabase { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityService"/> class.
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> object.</param>
        /// /// <param name="suow">The <see cref="ISyncUnitOfWork"/> object.</param>
        public IdentityService(IUnitOfWork uow, ISyncUnitOfWork suow)
        {
            Database = uow;
            SyncDatabase = suow;
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.Create(Identity)"/>.
        /// </summary>
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

        /// <summary>
        /// Implements <see cref="IIdentityService.Delete(Identity)"/>.
        /// </summary>
        public void Delete(Identity model)
        {
            Database.Identity.Delete(model.ID);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.Edit(Identity)"/>.
        /// </summary>
        public void Edit(Identity model)
        {
            Database.Identity.Update(model);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.Find(Identity)"/>.
        /// </summary>
        public IdentityDTO Find(Identity model)
        {
            return GetFull(model.GUID);
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.GenerateKey"/>.
        /// </summary>
        public void GenerateKey()
        {
            RSA rsa = new RSA();
            rsa.GenerateKey();
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.Get(int?)"/>.
        /// </summary>
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

        /// <summary>
        /// Implements <see cref="IIdentityService.GetAll"/>.
        /// </summary>
        public IEnumerable<IdentityDTO> GetAll()
        {
            List<IdentityDTO> result = new List<IdentityDTO>();
            foreach (var item in Database.Identity.GetAll())
            {
                result.Add(GetFull(item.GUID));
            }
            return result;
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.GetByDepartment(string)"/>.
        /// </summary>
        public IEnumerable<IdentityDTO> GetByDepartment(string department)
        {
            List<IdentityDTO> result = new List<IdentityDTO>();
            foreach (var item in SyncDatabase.User.GetAll().Where(x => x.Department == department))
            {
                result.Add(GetFull(item.GUID));
            }
            return result;
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.GetByGroup(string)"/>.
        /// </summary>
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
        /// Implements <see cref="IIdentityService.GetByGUID(string)"/>.
        /// </summary>
        public IdentityDTO GetByGUID(string guid)
        {
            return GetFull(guid);
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.GetByName(string, string, string)"/>.
        /// </summary>
        public IdentityDTO GetByName(string name, string midname, string surname)
        {
            User item = SyncDatabase.User.Find(x => (x.Surname == surname) && (x.Name == name) 
                && (x.Midname == midname)).FirstOrDefault();
            return GetFull(item.GUID);
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.GetByStatus(string)"/>.
        /// </summary>
        public IEnumerable<IdentityDTO> GetByStatus(string status)
        {
            List<IdentityDTO> result = new List<IdentityDTO>();
            foreach (var item in SyncDatabase.User.GetAll().Where(x => x.Status == status))
            {
                result.Add(GetFull(item.GUID));
            }
            return result;
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.GetFull(string)"/>.
        /// </summary>
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
        /// Implements <see cref="IIdentityService.GetGrasshopperKey(string)"/>.
        /// </summary>
        /// <param name="key">The client public key.</param>
        /// <returns>The cipherbytes.</returns>
        public byte[] GetGrasshopperKey(string key)
        {
            RSA rsa = new RSA();
            return rsa.ResponseKey(Keys.GrasshopperKey, key);
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.GetSimple(int?)"/>.
        /// </summary>
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

        /// <summary>
        /// Implements <see cref="IIdentityService.GetUser(string, string)"/>.
        /// </summary>
        public UserDTO GetUser(string identityValue, string domain)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, identityValue);
                if (user != null)
                {
                    return (UserDTO)user;
                }
                return null;
            }
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.GetUsers1C"/>.
        /// </summary>
        public IEnumerable<User> GetUsers1C()
        {
            return SyncDatabase.User.GetAll();
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.IsUserExist(string, string)"/>.
        /// </summary>
        public bool IsUserExist(string identityValue, string domain)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, identityValue);
                return user != null ? true : false;
            }
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.IsValidUser(string, string, string)"/>.
        /// </summary>
        public bool IsValidUser(string username, string password, string domain)
        {
            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                return context.ValidateCredentials(username.Trim(), password.Trim());
            }
        }

        /// <summary>
        /// Implements <see cref="IIdentityService.SaveImage(HttpPostedFileBase)"/>.
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

        /// <summary>
        ///  Represents a Owin <see cref="IAuthenticationManager"/>.
        /// </summary>
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
            /// Parse username and domain from string
            /// </summary>
            private class Parser
            {
                public string Username { get; set; }
                public string Domain { get; set; }
                private string Input { get; set; }

                public Parser() { }

                public Parser(string input)
                {
                    Input = input;
                }

                public Parser Parse()
                {
                    if (Input.Contains('\\'))
                    {
                        var parsed = Input.Split('\\');
                        return new Parser
                        {
                            Username = parsed[1],
                            Domain = parsed[0]
                        };
                    }
                    if (Input.Contains('@'))
                    {
                        var parsed = Input.Split('@');
                        return new Parser
                        {
                            Username = parsed[0],
                            Domain = parsed[1]
                        };
                    }
                    return null;
                }
            }

            /// <summary>
            /// Check if username and password matches existing account in ActiveDirectory. 
            /// </summary>
            public AuthenticationResult SignIn(string username, string password)
            {
                
                Parser parser = new Parser(username).Parse();
                string domain, name;
                if (parser != null)
                {
                    domain = parser.Domain;
                    name = parser.Username;
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
                    var identity = CreateIdentity(user, domain);

                    authenticationManager.SignOut(SSTUAuthentication.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
                    return new AuthenticationResult();
                }
            }

            /// <summary>
            /// Create new ClaimsIdentity to save it in cookie
            /// </summary>
            /// <param name="userPrincipal">User from ActiveDirectory</param>
            private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal, string domain)
            {
                var identity = new ClaimsIdentity(SSTUAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
                identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
                identity.AddClaim(new Claim(ClaimTypes.WindowsAccountName, userPrincipal.SamAccountName + "@" + domain));
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
    }
}