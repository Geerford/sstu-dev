using Service.Interfaces;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    [Authorize(Roles = "SSTU_Administrator")]
    public class AuditController : Controller
    {
        IAuditService service;

        public AuditController(IAuditService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View(service.GetAll());
        }
    }
}