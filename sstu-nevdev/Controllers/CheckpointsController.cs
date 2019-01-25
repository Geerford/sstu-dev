using Domain.Core;
using Service.DTO;
using Service.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class CheckpointsController : ApiController
    {
        ICheckpointService service;

        public CheckpointsController(ICheckpointService service)
        {
            this.service = service;
        }

        // GET api/checkpoints
        public IHttpActionResult Get()
        {
            IEnumerable<CheckpointDTO> items = service.GetAll();
            if (items != null)
            {
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/checkpoints/5
        public IHttpActionResult Get(int id)
        {
            CheckpointDTO item = service.Get(id);
            if (item != null)
            {
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/checkpoints
        [HttpPost]
        public IHttpActionResult Post([FromBody]CheckpointDTO item)
        {
            if (item != null)
            {
                service.Create(item);
                return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
            }
            return BadRequest();
        }

        // PUT api/checkpoints/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CheckpointDTO item)
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

        // DELETE api/checkpoints/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Checkpoint item = service.GetSimple(id);
            if (item != null)
            {
                service.DeleteAllAdmission(id);
                service.Delete(item);
                return Ok(item);
            }
            return BadRequest();
        }
    }
}