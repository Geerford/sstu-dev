using Domain.Core;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    //[AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator,SSTU_Inspector")]
    public class CheckpointsController : ApiController
    {
        ICheckpointService service;

        public CheckpointsController(ICheckpointService service)
        {
            this.service = service;
        }

        // GET api/checkpoints
        /// <summary>
        /// Gets all <see cref="CheckpointDTO"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="CheckpointDTO"/> from the repository.</returns>
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
        /// <summary>
        /// Gets a <see cref="CheckpointDTO"/> object from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="CheckpointDTO"/>.</param>
        /// <returns>The <see cref="CheckpointDTO"/> with the given ID.</returns>
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
        /// <summary>
        /// Creates the <see cref="Checkpoint"/> object in the repository.
        /// </summary>
        /// <param name="item">The <see cref="CheckpointDTO"/> object.</param>
        [HttpPost]
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator")]
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
        /// <summary>
        /// Edits the <see cref="CheckpointDTO"/> object in the repository.
        /// </summary>
        /// <param name="id">The checkpoint id.</param>
        /// <param name="item">The <see cref="CheckpointDTO"/> object.</param>
        [HttpPut]
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator")]
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
        /// <summary>
        /// Deletes the <see cref="Checkpoint"/> object in the repository.
        /// </summary>
        /// <param name="id">The checkpoint id.</param>
        [HttpDelete]
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator")]
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