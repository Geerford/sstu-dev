using Domain.Core;
using Service.Interfaces;
using System.Net;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    [Authorize(Roles = "SSTU_Administrator")]
    public class ModeController : Controller
    {
        IModeService service;

        public ModeController(IModeService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View(service.GetAll());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mode item = service.Get(id);
            if (item != null)
            {
                return PartialView(item);
            }
            return HttpNotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mode model)
        {
            if (string.IsNullOrEmpty(model.Description))
            {
                ModelState.AddModelError("Description", "Описание должно быть заполнено");
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                ModelState.AddModelError("Status", "Статус должен быть заполнен");
            }
            if (ModelState.IsValid)
            {
                service.Create(new Mode
                {
                    Description = model.Description,
                    Status = model.Status
                });
                return RedirectToAction("Index", "Mode");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mode model = service.Get(id);
            if (model != null)
            {
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mode model)
        {
            if (string.IsNullOrEmpty(model.Description))
            {
                ModelState.AddModelError("Description", "Описание должно быть заполнено");
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                ModelState.AddModelError("Status", "Статус должен быть заполнен");
            }
            if (ModelState.IsValid)
            {
                service.Edit(new Mode
                {
                    ID = model.ID,
                    Description = model.Description,
                    Status = model.Status
                });
                return RedirectToAction("Index", "Mode");
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mode model = service.Get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Mode model)
        {
            try
            {
                service.Delete(model);
                return RedirectToAction("Index", "Mode");
            }
            catch
            {
                return View(model);
            }
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }
    }
}