using Domain.Core;
using Service.DTO;
using Service.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class IdentitiesController : ApiController
    {
        IIdentityService service;

        public IdentitiesController(IIdentityService service)
        {
            this.service = service;
        }

        // GET api/identities
        public IEnumerable<IdentityDTO> Get()
        {
            return service.GetAll();
        }

        // GET api/identities/5
        public IdentityDTO Get(int id)
        {
            return service.Get(id);
        }

        // POST api/identities
        [HttpPost]
        public void Post([FromBody]Identity item)
        {
            service.Create(item);
        }

        // PUT api/identities/5
        [HttpPut]
        public void Put(int id, [FromBody]Identity item)
        {
            if(id == item.ID)
            {
                service.Edit(item);
            }
        }

        // DELETE api/identities/5
        [HttpDelete]
        public void Delete(int id)
        {
            Identity item = service.GetSimple(id);
            if (item != null)
            {
                service.Delete(item);
            }
        }
    }
}