﻿using Domain.Core;
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
        public IEnumerable<Role> Get()
        {
            return service.GetAll();
        }

        // GET api/roles/5
        public Role Get(int id)
        {
            return service.Get(id);
        }

        // POST api/roles
        [HttpPost]
        public void Post([FromBody]Role item)
        {
            service.Create(item);
        }

        // PUT api/roles/5
        [HttpPut]
        public void Put(int id, [FromBody]Role item)
        {
            if (id == item.ID)
            {
                service.Edit(item);
            }
        }

        // DELETE api/roles/5
        [HttpDelete]
        public void Delete(int id)
        {
            Role item = service.Get(id);
            if(item != null)
            {
                service.Delete(item);
            }
        }
    }
}