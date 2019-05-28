using Domain.Core;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Net;
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
        /// <summary>
        /// Gets all <see cref="Activity"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="Activity"/> from the repository.</returns>
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator,SSTU_Inspector,SSTU_Student")]
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
        /// <summary>
        /// Gets a <see cref="Activity"/> object from the repository.
        /// </summary>
        /// <param name="id">The ID of <see cref="Activity"/>.</param>
        /// <returns>The <see cref="Activity"/> with the given ID.</returns>
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator,SSTU_Inspector,SSTU_Student")]
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
        /// <summary>
        /// Creates the <see cref="Activity"/> object in the repository.
        /// </summary>
        /// <param name="item">The <see cref="ActivityAPIModel"/> object.</param>
        [HttpPost]
        [AllowAnonymous]
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
                            return Ok(identity);
                        case 500:
                            return Content(HttpStatusCode.BadRequest, "Permission denied");
                        default:
                            return Ok();
                    }
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Object does not exist");
                }
            }
            return BadRequest();
        }

        // PUT api/activities/5
        /// <summary>
        /// Edits the <see cref="Activity"/> object in the repository.
        /// </summary>
        /// <param name="id">The activity id.</param>
        /// <param name="item">The <see cref="Activity"/> object.</param>
        [HttpPut]
        [AuthenticationAPI(Roles = "SSTU_Administrator")]
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
        /// <summary>
        /// Deletes the <see cref="Activity"/> object in the repository.
        /// </summary>
        /// <param name="id">The activity id.</param>
        [HttpDelete]
        [AuthenticationAPI(Roles = "SSTU_Administrator")]
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