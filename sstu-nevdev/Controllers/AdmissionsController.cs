using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    [AuthenticationAPI(Roles = "SSTU_Administrator")]
    public class AdmissionsController : ApiController
    {
        IAdmissionService service;

        public AdmissionsController(IAdmissionService service)
        {
            this.service = service;
        }

        // GET api/admissions
        public IHttpActionResult Get()
        {
            IEnumerable<Admission> items = service.GetAll();
            if (items != null)
            {
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/admissions/5
        public IHttpActionResult Get(int id)
        {
            Admission item = service.Get(id);
            if (item != null)
            {
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/admissions
        [HttpPost]
        public IHttpActionResult Post([FromBody]Admission item)
        {
            if (item != null)
            {
                service.Create(item);
                return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
            }
            return BadRequest();
        }

        // PUT api/admissions/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Admission item)
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

        // DELETE api/admissions/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Admission item = service.Get(id);
            if (item != null)
            {
                service.Delete(item);
                return Ok(item);
            }
            return BadRequest();
        }
    }
}