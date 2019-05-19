using Domain.Core.Logs;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    [AuthenticationAPI(Roles = "SSTU_Administrator")]
    public class AuditsController : ApiController
    {
        IAuditService service;

        public AuditsController(IAuditService service)
        {
            this.service = service;
        }

        // GET api/audits
        /// <summary>
        /// Gets all <see cref="Audit"/> from repository.
        /// </summary>
        /// <returns>The collection of all <see cref="Audit"/> from the repository.</returns>
        public IHttpActionResult Get()
        {
            IEnumerable<Audit> items = service.GetAll();
            if (items != null)
            {
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }
    }
}