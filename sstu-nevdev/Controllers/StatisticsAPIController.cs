using Domain.Core;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using sstu_nevdev.Models;
using System;
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

        [HttpPost]
        [Route("")]
        public IHttpActionResult GetStatiscs([FromBody]StatisticsAPIModel item)
        {
            if (item != null)
            {
                var items = service.GetStatistics(item.Campus, item.Row, item.Classroom, item.Name, item.Midname,  item.Surname, item.Start, item.End);
                if (items != null)
                {
                    return Ok(items);
                }
                return Content(HttpStatusCode.BadRequest, "Object does not exist");
            }
            return BadRequest();
        }

        // POST api/statistics/campus
        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by campus and time interval [start, end].
        /// </summary>
        /// <param name="item">The <see cref="CampusStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given campus and time interval [start, end].</returns>
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
        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by campus, row and time interval [start, end].
        /// </summary>
        /// <param name="item">The <see cref="CampusRowStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given campus, row and time interval [start, end].</returns>
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
        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by classroom and time interval [start, end].
        /// </summary>
        /// <param name="item">The <see cref="ClassroomStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given classroom and time interval [start, end].</returns>
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
        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by group and time interval [start, end].
        /// </summary>
        /// <param name="item">The <see cref="GroupStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given group and time interval [start, end].</returns>
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
        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by section and time interval [start, end].
        /// </summary>
        /// <param name="item">The <see cref="SectionStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given section and time interval [start, end].</returns>
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
        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by full name and time interval [start, end].
        /// </summary>
        /// <param name="item">The <see cref="UserStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given full name and time interval [start, end].</returns>
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
        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by GUID and time interval [start, end].
        /// </summary>
        /// <param name="item">The <see cref="GUIDStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given full name and time interval [start, end].</returns>
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
        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository by campus.
        /// </summary>
        /// <param name="item">The <see cref="CurrentCampusStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> with the given campus.</returns>
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
        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository by campus and row.
        /// </summary>
        /// <param name="item">The <see cref="CurrentCampusRowStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> with the given campus and row.</returns>
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
        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository by classroom.
        /// </summary>
        /// <param name="item">The <see cref="CurrentClassroomStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> with the given classroom.</returns>
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
        /// <summary>
        /// Gets all <see cref="IdentityDTO"/> from repository by section.
        /// </summary>
        /// <param name="item">The <see cref="CurrentSectionStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="IdentityDTO"/> with the given section.</returns>
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
        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by full name.
        /// </summary>
        /// <param name="item">The <see cref="LocationNameStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given full name.</returns>
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
        /// <summary>
        /// Gets all <see cref="Activity"/> from repository by GUID.
        /// </summary>
        /// <param name="item">The <see cref="LocationGUIDStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="Activity"/> with the given full name.</returns>
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
        /// <summary>
        /// Gets all <see cref="string"/> roles from active directory repository.
        /// </summary>
        /// <param name="item">The <see cref="ActiveDirectoryUsersStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="string"/> roles.</returns>
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
        /// <summary>
        /// Gets all <see cref="ADUserDTO"/> from active directory repository.
        /// </summary>
        /// <param name="item">The <see cref="ActiveDirectoryUsersStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="ADUserDTO"/>.</returns>
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
        /// <summary>
        /// Gets all <see cref="ADUserDTO"/> from repository by role.
        /// </summary>
        /// <param name="item">The <see cref="ActiveDirectoryRolesStatisticsAPIModel"/> object.</param>
        /// <returns>The collection of all <see cref="ADUserDTO"/> with the given.</returns>
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