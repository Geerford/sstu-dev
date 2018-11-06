using Service.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class RolesController : ApiController
    {
        IRoleService service;
        
        public RolesController(IRoleService service)
        {
            this.service = service;
        }

        // GET api/roles
        public IEnumerable<Domain.Core.Role> Get()
        {
            return service.GetAll();
        }

        // GET api/roles/5
        public Domain.Core.Role Get(int id)
        {
            return service.Get(id);
        }

        // POST api/roles
        public void Post([FromBody]string value)
        {
        }

        // PUT api/roles/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/roles/5
        public void Delete(int id)
        {
        }
    }
}
