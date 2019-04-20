using Service.Interfaces;
using System;
using System.IO;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class DatabasesController : ApiController
    {
        IDatabaseService service;

        public DatabasesController(IDatabaseService service)
        {
            this.service = service;
        }

        // POST api/databases/backup
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
        [HttpPost]
        [Route("api/databases/recovery")]
        public IHttpActionResult Recovery([FromBody]string item)
        {
            bool isExist = File.Exists(AppDomain.CurrentDomain.GetData("DataDirectory")
                            .ToString() + "\\" + item);
            if (isExist)
            {
                service.Recovery(item);
                return Ok();
            }
            return BadRequest();
        }
    }
}
