using Service.Interfaces;
using sstu_nevdev.App_Start;
using System;
using System.IO;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    //[AuthenticationAPI(Roles = "SSTU_Administrator")]
    public class DatabasesController : ApiController
    {
        IDatabaseService service;

        public DatabasesController(IDatabaseService service)
        {
            this.service = service;
        }

        // POST api/databases/backup
        /// <summary>
        /// Creates a database backup. 
        /// </summary>
        /// <returns>The backup name.</returns>
        [HttpPost]
        [Route("api/databases/backup")]
        public IHttpActionResult Backup()
        {
            string backupName = service.Backup();
            if (!string.IsNullOrEmpty(backupName))
            {
                return Ok(backupName);
            }
            return BadRequest();
        }

        // POST api/databases/recovery
        /// <summary>
        /// Recovers database from backup.
        /// </summary>
        /// <param name="backupName">The database backup name.</param>
        [HttpPost]
        [Route("api/databases/recovery")]
        public IHttpActionResult Recovery([FromBody]string item)
        {
            bool isExist = File.Exists(AppDomain.CurrentDomain.GetData("DataDirectory")
                            .ToString() + "\\" + item);
            if (isExist && service.Recovery(item))
            {
                return Ok();
            }
            return BadRequest();
        }

        // POST api/databases/sync
        /// <summary>
        /// Drops synchronized database.
        /// </summary>
        [HttpPost]
        [Route("api/databases/sync")]
        public IHttpActionResult Sync()
        {
            if (service.Sync())
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
