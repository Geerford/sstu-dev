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
        /// <summary>
        /// Gets all <see cref="Admission"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="Admission"/> from the repository.</returns>
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
        /// <summary>
        /// Gets a <see cref="Admission"/> object from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="Admission"/>.</param>
        /// <returns>The <see cref="Admission"/> with the given ID.</returns>
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
        /// <summary>
        /// Creates the <see cref="Admission"/> object in the repository.
        /// </summary>
        /// <param name="item">The <see cref="Admission"/> object.</param>
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
        /// <summary>
        /// Edits the <see cref="Admission"/> object in the repository.
        /// </summary>
        /// <param name="id">The admission id.</param>
        /// <param name="item">The <see cref="Admission"/> object.</param>
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
        /// <summary>
        /// Deletes the <see cref="Admission"/> object in the repository.
        /// </summary>
        /// <param name="id">The admission id.</param>
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