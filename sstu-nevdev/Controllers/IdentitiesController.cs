using Domain.Core;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    [AuthenticationAPI(Roles = "SSTU_Deanery, SSTU_Administrator, SSTU_Inspector")]
    public class IdentitiesController : ApiController
    {
        IIdentityService service;

        public IdentitiesController(IIdentityService service)
        {
            this.service = service;
        }

        // GET api/identities
        public IHttpActionResult Get()
        {
            IEnumerable<IdentityDTO> items = service.GetAll();
            if (items != null)
            {
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/identities/5
        public IHttpActionResult Get(int id)
        {
            IdentityDTO item = service.Get(id);
            if (item != null)
            {
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/identities
        [HttpPost]
        [AuthenticationAPI(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public IHttpActionResult Post([FromBody]Identity item)
        {
            if(item != null)
            {
                service.Create(item);
                return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
            }
            return BadRequest();
        }

        // PUT api/identities/5
        [HttpPut]
        [AuthenticationAPI(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public IHttpActionResult Put(int id, [FromBody]Identity item)
        {
            if (item != null)
            {
                if (id == item.ID)
                {
                    service.Edit(item);
                    return Ok(item);
                }
            }
            return BadRequest();
        }

        // DELETE api/identities/5
        [HttpDelete]
        [AuthenticationAPI(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public IHttpActionResult Delete(int id)
        {
            Identity item = service.GetSimple(id);
            if (item != null)
            {
                service.Delete(item);
                return Ok(item);
            }
            return BadRequest();
        }
    }
}