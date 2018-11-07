using Domain.Core;
using Service.Interfaces;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    public class AdmissionController : Controller
    {
        IAdmissionService service;

        public AdmissionController(IAdmissionService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View(service.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View(service.Get(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Admission model)
        {
            try
            {
                service.Create(model);
                return RedirectToAction("Index", "Admission", new { });
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            return View(service.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Admission model)
        {
            try
            {
                service.Edit(model);
                return RedirectToAction("Index", "Admission", new { });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(service.Get(id));
        }

        [HttpPost]
        public ActionResult Delete(Admission model)
        {
            try
            {
                service.Delete(model);
                return RedirectToAction("Index", "Admission", new { });
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }
    }
}