using Domain.Core;
using Service.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class AdmissionsController : ApiController
    {
        IAdmissionService service;

        public AdmissionsController(IAdmissionService service)
        {
            this.service = service;
        }

        // GET api/admissions
        public IEnumerable<Admission> Get()
        {
            return service.GetAll();
        }

        // GET api/admissions/5
        public Admission Get(int id)
        {
            return service.Get(id);
        }

        // POST api/admissions
        [HttpPost]
        public void Post([FromBody]Admission item)
        {
            service.Create(item);
        }

        // PUT api/admissions/5
        [HttpPut]
        public void Put(int id, [FromBody]Admission item)
        {
            if (id == item.ID)
            {
                service.Edit(item);
            }
        }

        // DELETE api/admissions/5
        [HttpDelete]
        public void Delete(int id)
        {
            Admission item = service.Get(id);
            if (item != null)
            {
                service.Delete(item);
            }
        }
    }
}