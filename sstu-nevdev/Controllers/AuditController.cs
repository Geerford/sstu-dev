using Domain.Core.Logs;
using PagedList;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(service.GetAll().OrderByDescending(x => x.ModifiedDate)
                .ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string query, int? page)
        {
            ViewBag.Query = query;
            string[] parsedQuery = query.Split(null);
            List<Audit> result = new List<Audit>();
            foreach (var item in service.GetAll())
            {
                foreach (var value in parsedQuery)
                {
                    if (Comparator.PropertiesThatContainText(item, value))
                    {
                        result.Add(item);
                        break;
                    }
                }
            }
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(result.OrderBy(x => x.ModifiedDate).ToPagedList(pageNumber, pageSize));
        }
    }
}