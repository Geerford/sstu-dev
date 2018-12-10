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
        public IEnumerable<CheckpointDTO> Get()
        {
            return service.GetAll();
        }

        // GET api/checkpoints/5
        public CheckpointDTO Get(int id)
        {
            return service.Get(id);
        }

        // POST api/checkpoints
        [HttpPost]
        public void Post([FromBody]CheckpointDTO checkpoint)
        {
            service.Create(checkpoint);
        }

        // PUT api/checkpoints/5
        [HttpPut]
        public void Put(int id, [FromBody]CheckpointDTO checkpoint)
        {
            if (id == checkpoint.ID)
            {
                service.Edit(checkpoint);
            }
        }

        // DELETE api/checkpoints/5
        [HttpDelete]
        public void Delete(int id)
        {
            Checkpoint item = service.GetSimple(id);
            if (item != null)
            {
                service.Delete(item);
            }
        }
    }
}