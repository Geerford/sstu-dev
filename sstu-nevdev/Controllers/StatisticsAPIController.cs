using Service.Interfaces;
using sstu_nevdev.App_Start;
using sstu_nevdev.Models;
using System.Net;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    [AuthenticationAPI(Roles = "SSTU_Inspector,SSTU_Administrator")]
    [RoutePrefix("api/statistics")]
    public class StatisticsAPIController : ApiController
    {
        IStatisticsService service;
        public StatisticsAPIController()
        {
        }
        public StatisticsAPIController(IStatisticsService service)
        {
            this.service = service;
        }

        // POST api/statistics/campus
        [HttpPost]
        [Route("campus")]
        public IHttpActionResult GetByCampus([FromBody]CampusStatisticsAPIModel item)
        {
            if (item != null && item.Campus != null && item.Start != null && item.End != null)
            {
                var items = service.GetByCampus((int)item.Campus, item.Start, item.End);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/campus/row
        [HttpPost]
        [Route("campus/row")]
        public IHttpActionResult GetByCampusRow([FromBody]CampusRowStatisticsAPIModel item)
        {
            if (item != null && item.Campus != null && item.Row != null && item.Start != null && item.End != null)
            {
                var items = service.GetByCampusRow((int)item.Campus, (int)item.Row, item.Start, item.End);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/classroom
        [HttpPost]
        [Route("classroom")]
        public IHttpActionResult GetByClassroom([FromBody]ClassroomStatisticsAPIModel item)
        {
            if (item != null)
            {
                if (item.Classroom != null && item.Start != null && item.End != null)
                {
                    var items = service.GetByClassroom((int)item.Classroom, item.Start, item.End);
                    if (items != null)
                    {
                        return Ok(items);
                    }
                }
                    
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/group
        [HttpPost]
        [Route("group")]
        public IHttpActionResult GetByGroup([FromBody]GroupStatisticsAPIModel item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Group) && item.Start != null && item.End != null)
            {
                var items = service.GetByGroup(item.Group, item.Start, item.End);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/section
        [HttpPost]
        [Route("section")]
        public IHttpActionResult GetBySection([FromBody]SectionStatisticsAPIModel item)
        {
            if (item != null && item.Section != null && item.Start != null && item.End != null)
            {
                var items = service.GetBySection((int)item.Section, item.Start, item.End);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/user
        [HttpPost]
        [Route("user")]
        public IHttpActionResult GetByUser([FromBody]UserStatisticsAPIModel item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Name) && !string.IsNullOrEmpty(item.Surname)
                    && !string.IsNullOrEmpty(item.Midname) && item.Start != null && item.End != null)
            {
                var items = service.GetByUser(item.Name, item.Midname, item.Surname, item.Start, item.End);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/guid
        [HttpPost]
        [Route("guid")]
        public IHttpActionResult GetByUserGuid([FromBody]GUIDStatisticsAPIModel item)
        {
            if (item != null && !string.IsNullOrEmpty(item.GUID) && item.Start != null && item.End != null)
            {
                var items = service.GetByUser(item.GUID, item.Start, item.End);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/current/campus
        [HttpPost]
        [Route("current/campus")]
        public IHttpActionResult GetCurrentByCampus([FromBody]CurrentCampusStatisticsAPIModel item)
        {
            if (item != null && item.Campus != null)
            {
                var items = service.GetCurrentByCampus((int)item.Campus);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/current/campus/row
        [HttpPost]
        [Route("current/campus/row")]
        public IHttpActionResult GetCurrentByCampusRow([FromBody]CurrentCampusRowStatisticsAPIModel item)
        {
            if (item != null && item.Campus != null && item.Row != null)
            {
                var items = service.GetCurrentByCampusRow((int)item.Campus, (int)item.Row);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/current/classroom
        [HttpPost]
        [Route("current/classroom")]
        public IHttpActionResult GetCurrentByClassroom([FromBody]CurrentClassroomStatisticsAPIModel item)
        {
            if (item != null && item.Classroom != null)
            {
                var items = service.GetCurrentByClassroom((int)item.Classroom);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/current/section
        [HttpPost]
        [Route("current/section")]
        public IHttpActionResult GetCurrentBySection([FromBody]CurrentSectionStatisticsAPIModel item)
        {
            if (item != null && item.Section != null)
            {
                var items = service.GetCurrentBySection((int)item.Section);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/current/user
        [HttpPost]
        [Route("current/user")]
        public IHttpActionResult GetLocationByName([FromBody]LocationNameStatisticsAPIModel item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Name) && !string.IsNullOrEmpty(item.Surname)
                    && !string.IsNullOrEmpty(item.Midname))
            {
                var items = service.GetUserLocation(item.Name, item.Midname, item.Surname);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/current/guid
        [HttpPost]
        [Route("current/guid")]
        public IHttpActionResult GetLocationByGUID([FromBody]LocationGUIDStatisticsAPIModel item)
        {
            if (item != null && !string.IsNullOrEmpty(item.GUID))
            {
                var items = service.GetUserLocation(item.GUID);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/roles
        [HttpPost]
        [Route("roles")]
        public IHttpActionResult GetRoles([FromBody]ActiveDirectoryUsersStatisticsAPIModel item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Domain))
            {
                var items = service.GetRoles(item.Domain);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/users
        [HttpPost]
        [Route("users")]
        public IHttpActionResult GetUsers([FromBody]ActiveDirectoryUsersStatisticsAPIModel item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Domain))
            {
                var items = service.GetUsers(item.Domain);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/users/role
        [HttpPost]
        [Route("users/role")]
        public IHttpActionResult GetUsersByRole([FromBody]ActiveDirectoryRolesStatisticsAPIModel item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Domain) && !string.IsNullOrEmpty(item.Role))
            {
                var items = service.GetUsersByRole(item.Domain, item.Role);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }
    }
}