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
        ITypeService typeService;

        public ActivitiesController(IActivityService activityService, ICheckpointService checkpointService, 
            IIdentityService identityService, ITypeService typeService)
        {
            this.activityService = activityService;
            this.checkpointService = checkpointService;
            this.identityService = identityService;
            this.typeService = typeService;
        }

        // GET api/activities
        public IEnumerable<Activity> Get()
        {
            return activityService.GetAll();
        }

        // GET api/activities/5
        public Activity Get(int id)
        {
            return activityService.Get(id);
        }

        // POST api/activities
        [HttpPost]
        public IHttpActionResult Post([FromBody]ActivityModel model)
        {
            CheckpointDTO checkpoint = checkpointService.Get(model.CheckpointID);
            IdentityDTO identity;
            if (model.RFID.Length != 0)
            {
                identity = identityService.GetByRFID(model.RFID);
            }
            else
            {
                identity = identityService.GetByQR(model.QR);
            }
            if(identity != null)
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
            return Json("Object does not exist");
        }

        // PUT api/activities/5
        [HttpPut]
        public void Put(int id, [FromBody]Activity item)
        {
            if (id == item.ID)
            {
                activityService.Edit(item);
            }
        }

        // DELETE api/activities/5
        [HttpDelete]
        public void Delete(int id)
        {
            Activity item = activityService.Get(id);
            if (item != null)
            {
                activityService.Delete(item);
            }
        }
    }
}