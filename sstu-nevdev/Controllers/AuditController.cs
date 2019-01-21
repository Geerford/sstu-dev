using Repository.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    public class AuditController : Controller
    {
        IUnitOfWork auditRepository;
        public AuditController(IUnitOfWork auditRepository)
        {
            this.auditRepository = auditRepository;
        }

        public ActionResult Index()
        {
            return View(auditRepository.Audit.GetAll().ToList());
        }
    }
}