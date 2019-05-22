using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Net;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    [Authorize(Roles = "SSTU_Administrator")]
    public class TypeController : Controller
    {
        ITypeService service;

        public TypeController(ITypeService service)
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
            var item = service.Get(id);
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
        public ActionResult Create(TypeViewModel model)
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
                service.Create(new Type {
                    Description = model.Description,
                    Status = model.Status
                });
                return RedirectToAction("Index", "Type");
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type model = service.Get(id);
            if(model != null)
            {
                return View(new TypeViewModel
                {
                    ID = model.ID,
                    Description = model.Description,
                    Status = model.Status
                });
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TypeViewModel model)
        {
            if (string.IsNullOrEmpty(model.Description))
            {
                ModelState.AddModelError("Description", "Описание должно быть заполнено");
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                ModelState.AddModelError("Status", "Выберите статус");
            }
            if (ModelState.IsValid)
            {
                service.Edit(new Type
                {
                    ID = model.ID,
                    Description = model.Description,
                    Status = model.Status
                });
                return RedirectToAction("Index", "Type");
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type model = service.Get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Type model)
        {
            try
            {
                service.Delete(model);
                return RedirectToAction("Index", "Type");
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