using Domain.Core;
using Service.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;
using static sstu_nevdev.Models.CheckpointModel;

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
            ViewBag.Status = new SelectList(new List<StatusForList> {
                new StatusForList { Key = "Пропускной", Display = "Пропускной" },
                new StatusForList { Key = "Лекционный", Display = "Лекционный" },
                new StatusForList { Key = "Статистический", Display = "Статистический" } }, "Key", "Display");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Type model, string StatusList)
        {
            try
            {
                model.Status = StatusList;
                service.Create(model);
                return RedirectToAction("Index", "Type");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Status = new SelectList(new List<StatusForList> {
                new StatusForList { Key = "Пропускной", Display = "Пропускной" },
                new StatusForList { Key = "Лекционный", Display = "Лекционный" },
                new StatusForList { Key = "Статистический", Display = "Статистический" } }, "Key", "Display");
            return View(service.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Type model, string StatusList)
        {
            try
            {
                if (!string.IsNullOrEmpty(StatusList))
                {
                    model.Status = StatusList;
                }
                service.Edit(model);
                return RedirectToAction("Index", "Type");
            }
            catch
            {
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