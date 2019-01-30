using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    [AuthenticationAPI(Roles = "SSTU_Administrator")]
    public class TypesController : ApiController
    {
        ITypeService service;

        public TypesController(ITypeService service)
        {
            this.service = service;
        }

        // GET api/types
        public IHttpActionResult Get()
        {
            IEnumerable<Type> items = service.GetAll();
            if (items != null)
            {
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/types/5
        public IHttpActionResult Get(int id)
        {
            Type item = service.Get(id);
            if (item != null)
            {
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/types
        [HttpPost]
        public IHttpActionResult Post([FromBody]Type item)
        {
            if (item != null)
            {
                service.Create(item);
                return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
            }
            return BadRequest();
        }

        // PUT api/types/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Type item)
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

        // DELETE api/types/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Type item = service.Get(id);
            if (item != null)
            {
                service.Delete(item);
                return Ok(item);
            }
            return BadRequest();
        }
    }
}