using Domain.Core;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class ActivitiesController : ApiController
    {
        IActivityService activityService;
        ICheckpointService checkpointService;
        IIdentityService identityService;

        public ActivitiesController(IActivityService activityService, ICheckpointService checkpointService, 
            IIdentityService identityService)
        {
            this.activityService = activityService;
            this.checkpointService = checkpointService;
            this.identityService = identityService;
        }

        // GET api/activities
        public IHttpActionResult Get()
        {
            IEnumerable<Activity> items = activityService.GetAll();
            if (items != null)
            {
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/activities/5
        public IHttpActionResult Get(int id)
        {
            Activity item = activityService.Get(id);
            if (item != null)
            {
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/activities
        [HttpPost]
        public IHttpActionResult Post([FromBody]ActivityAPIModel item)
        {
            if (item != null)
            {
                CheckpointDTO checkpoint = checkpointService.GetByIP(item.CheckpointIP);
                IdentityDTO identity = null;
                if (!string.IsNullOrEmpty(item.GUID))
                {
                    identity = identityService.GetByGUID(item.GUID);
                }
                else
                {
                    return BadRequest();
                }
                if (identity != null)
                {
                    string picturePath = "/Content/uploads/" + identity.Picture;
                    int code = activityService.IsOk(checkpoint, identity);
                    switch (code)
                    {
                        case 200:
                            return Json(identity);
                        case 500:
                            return Json("Permission denied");
                        default:
                            return Ok();
                    }
                }
                else
                {
                    return Json("Object does not exist");
                }
            }
            return BadRequest();
        }

        // PUT api/activities/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Activity item)
        {
            if (item != null)
            {
                if (id == item.ID)
                {
                    activityService.Edit(item);
                    return Ok(item);
                }
            }
            return BadRequest();
        }

        // DELETE api/activities/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Activity item = activityService.Get(id);
            if (item != null)
            {
                activityService.Delete(item);
                return Ok(item);
            }
            return BadRequest();
        }
    }
}