using Domain.Core;
using Newtonsoft.Json.Linq;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator,SSTU_Inspector,SSTU_Security")]
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
        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="IdentityDTO"/> from the repository.</returns>
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator,SSTU_Inspector")]
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
        /// <summary>
        /// Gets a <see cref="IdentityDTO"/> object from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="IdentityDTO"/>.</param>
        /// <returns>The <see cref="IdentityDTO"/> with the given ID.</returns>
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
        /// <summary>
        /// Creates the <see cref="Identity"/> object in the repository.
        /// </summary>
        /// <param name="item">The <see cref="Identity"/> object.</param>
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

        // POST api/identities
        /// <summary>
        /// Saves the <see cref="HttpPostedFileBase"/> file and assigns it to <see cref="Identity"/> object.
        /// </summary>
        /// <param name="item">The <see cref="IdentityAPIModel"/> object.</param>
        [HttpPost]
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator")]
        [Route("image")]
        public IHttpActionResult SaveImage([FromBody]IdentityAPIModel item)
        {
            if (item != null && item.Identity != null && item.File != null)
            {
                if (!string.IsNullOrEmpty(item.Identity.Picture))
                {
                    item.Identity.Picture = service.SaveImage(item.File, item.Identity.Picture);
                }
                else
                {
                    item.Identity.Picture = service.SaveImage(item.File, null);
                }
                service.Edit(item.Identity);
                return Ok(item.Identity);
            }
            return BadRequest();
        }

        // PUT api/identities/5
        /// <summary>
        /// Edits the <see cref="Identity"/> object in the repository.
        /// </summary>
        /// <param name="id">The identity id.</param>
        /// <param name="item">The <see cref="Identity"/> object.</param>
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
        /// <summary>
        /// Deletes the <see cref="Identity"/> object in the repository.
        /// </summary>
        /// <param name="id">The identity id.</param>
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