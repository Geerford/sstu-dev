using Domain.Core;
using Service.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class TypesController : ApiController
    {
        ITypeService service;

        public TypesController(ITypeService service)
        {
            this.service = service;
        }

        // GET api/types
        public IEnumerable<Type> Get()
        {
            return service.GetAll();
        }

        // GET api/types/5
        public Type Get(int id)
        {
            return service.Get(id);
        }

        // POST api/types
        [HttpPost]
        public void Post([FromBody]Type item)
        {
            service.Create(item);
        }

        // PUT api/types/5
        [HttpPut]
        public void Put(int id, [FromBody]Type item)
        {
            if (id == item.ID)
            {
                service.Edit(item);
            }
        }

        // DELETE api/types/5
        [HttpDelete]
        public void Delete(int id)
        {
            Type item = service.Get(id);
            if (item != null)
            {
                service.Delete(item);
            }
        }
    }
}