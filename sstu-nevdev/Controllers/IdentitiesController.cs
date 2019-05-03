using Domain.Core;
using Newtonsoft.Json.Linq;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    //[AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator,SSTU_Inspector,SSTU_Security")]
    public class IdentitiesController : ApiController
    {
        IIdentityService service;
        IAESService AESService;

        public IdentitiesController(IIdentityService service, IAESService AESService)
        {
            this.service = service;
            this.AESService = AESService;
        }

        // GET api/identities
        //[AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator,SSTU_Inspector")]
        public IHttpActionResult Get()
        {
            IEnumerable<IdentityDTO> items = service.GetAll();
            if (items != null)
            {
                dynamic json = new JObject();
                json["items"] = JToken.FromObject(items);
                return Ok(AESService.Encrypt(json));
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
                dynamic json = new JObject();
                json["item"] = JToken.FromObject(item);
                return Ok(AESService.Encrypt(json));
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/identities
        [HttpPost]
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator")]
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
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator")]
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
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator")]
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