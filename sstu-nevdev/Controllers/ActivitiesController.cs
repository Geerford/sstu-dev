using Domain.Core;
using Service.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class ActivitiesController : ApiController
    {
        IActivityService service;

        public ActivitiesController(IActivityService service)
        {
            this.service = service;
        }

        // GET api/activities
        public IEnumerable<Activity> Get()
        {
            return service.GetAll();
        }

        // GET api/activities/5
        public Activity Get(int id)
        {
            return service.Get(id);
        }

        // POST api/activities
        [HttpPost]
        public void Post([FromBody]Activity item)
        {
            service.Create(item);
        }

        // PUT api/activities/5
        [HttpPut]
        public void Put(int id, [FromBody]Activity item)
        {
            if (id == item.ID)
            {
                service.Edit(item);
            }
        }

        // DELETE api/activities/5
        [HttpDelete]
        public void Delete(int id)
        {
            Activity item = service.Get(id);
            if (item != null)
            {
                service.Delete(item);
            }
        }
    }
}