using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class IdentitiesController : ApiController
    {
        IIdentityService identityService;

        public IdentitiesController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        // GET api/identities
        public IEnumerable<Identity> Get()
        {
            return identityService.GetAll();
        }

        // GET api/identities/5
        public Identity Get(int id)
        {
            return identityService.Get(id);
        }

        // POST api/identities
        [HttpPost]
        public void Post([FromBody]IdentityModel item)
        {
            identityService.Create(new Identity
            {
                RFID = item.RFID,
                QR = item.QR,
                Name = item.Name,
                Surname = item.Surname,
                Midname = item.Midname,
                Gender = item.Gender,
                Birthdate = item.Birthdate,
                Picture = item.Picture,
                Country = item.Country,
                City = item.City,
                Phone = item.Phone,
                Email = item.Email,
                Department = item.Department,
                Group = item.Group,
                Status = item.Status,
                CreatedBy = item.CreatedBy,
                UpdatedBy = item.UpdatedBy,
                RoleID = item.RoleID
            });
        }

        // PUT api/identities/5
        [HttpPut]
        public void Put(int id, [FromBody]IdentityModel item)
        {
            if (id == item.ID)
            {
                Identity identity = identityService.Get(item.ID);
                if (item.RFID != null)
                {
                    identity.RFID = item.RFID;
                }
                if (item.QR != null)
                {
                    identity.QR = item.QR;
                }
                if (item.Name != null)
                {
                    identity.Name = item.Name;
                }
                if (item.Surname != null)
                {
                    identity.Surname = item.Surname;
                }
                if (item.Midname != null)
                {
                    identity.Midname = item.Midname;
                }
                if (item.Gender != false)
                {
                    identity.Gender = item.Gender;
                }
                if (item.Birthdate != null)
                {
                    identity.Birthdate = item.Birthdate;
                }
                if (item.Picture != null)
                {
                    identity.Picture = item.Picture;
                }
                if (item.Country != null)
                {
                    identity.Country = item.Country;
                }
                if (item.City != null)
                {
                    identity.City = item.City;
                }
                if (item.Phone != null)
                {
                    identity.Phone = item.Phone;
                }
                if (item.Email != null)
                {
                    identity.Email = item.Email;
                }
                if (item.Department != null)
                {
                    identity.Department = item.Department;
                }
                if (item.Group != null)
                {
                    identity.Group = item.Group;
                }
                if (item.Status != null)
                {
                    identity.Status = item.Status;
                }
                if (item.CreatedBy != null)
                {
                    identity.CreatedBy = item.CreatedBy;
                }
                if (item.UpdatedBy != null)
                {
                    identity.UpdatedBy = item.UpdatedBy;
                }
                if (item.RoleID != 0)
                {
                    identity.RoleID = item.RoleID;
                }
                identityService.Edit(identity);
            }
        }

        // DELETE api/identities/5
        [HttpDelete]
        public void Delete(int id)
        {
            Identity item = identityService.Get(id);
            if (item != null)
            {
                identityService.Delete(item);
            }
        }
    }
}