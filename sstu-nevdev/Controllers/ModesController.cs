using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    [AuthenticationAPI(Roles = "SSTU_Administrator")]
    public class ModesController : ApiController
    {
        IModeService service;

        public ModesController(IModeService service)
        {
            this.service = service;
        }

        // GET api/modes
        public IHttpActionResult Get()
        {
            IEnumerable<Mode> items = service.GetAll();
            if (items != null)
            {
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/modes/5
        public IHttpActionResult Get(int id)
        {
            Mode item = service.Get(id);
            if (item != null)
            {
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/modes
        [HttpPost]
        public IHttpActionResult Post([FromBody]Mode item)
        {
            if (item != null)
            {
                service.Create(item);
                return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
            }
            return BadRequest();
        }

        // PUT api/modes/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Mode item)
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

        // DELETE api/modes/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Mode item = service.Get(id);
            if (item != null)
            {
                service.Delete(item);
                return Ok(item);
            }
            return BadRequest();
        }
    }
}