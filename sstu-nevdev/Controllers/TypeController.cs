using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
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

        public ActionResult Details(int id)
        {
            return View(service.Get(id));
        }

        public ActionResult Create()
        {
            TypeViewModel model = new TypeViewModel
            {
                StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Пропускной",
                        Display = "Пропускной" },
                    new StatusForList {
                        Key = "Лекционный",
                        Display = "Лекционный" },
                    new StatusForList {
                        Key = "Статистический",
                        Display = "Статистический" } },
                    "Key", "Display")
            };
            return View(model);
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
                ModelState.AddModelError("Status", "Выберите статус");
            }
            if (ModelState.IsValid)
            {
                service.Create(new Type {
                    Description = model.Description,
                    Status = model.Status
                });
                return RedirectToAction("Index", "Type");
            }
            else
            {
                model.StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Пропускной",
                        Display = "Пропускной" },
                    new StatusForList {
                        Key = "Лекционный",
                        Display = "Лекционный" },
                    new StatusForList {
                        Key = "Статистический",
                        Display = "Статистический" } },
                    "Key", "Display");
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            TypeViewModel model = (TypeViewModel)service.Get(id);
            model.StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Пропускной",
                        Display = "Пропускной" },
                    new StatusForList {
                        Key = "Лекционный",
                        Display = "Лекционный" },
                    new StatusForList {
                        Key = "Статистический",
                        Display = "Статистический" } },
                    "Key", "Display");
            return View(model);
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
            else
            {
                model.StatusList = model.StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Пропускной",
                        Display = "Пропускной" },
                    new StatusForList {
                        Key = "Лекционный",
                        Display = "Лекционный" },
                    new StatusForList {
                        Key = "Статистический",
                        Display = "Статистический" } },
                    "Key", "Display");
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(service.Get(id));
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