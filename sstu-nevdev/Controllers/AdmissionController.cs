using Domain.Core;
using Service.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;
using static sstu_nevdev.Models.CheckpointModel;

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
            ViewBag.Role = new SelectList(new List<StatusForList> { new StatusForList { Key = "Сотрудник", Display = "Сотрудник" },
                new StatusForList { Key = "Студент", Display = "Студент" } }, "Key", "Display");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Admission model, string RoleList)
        {
            try
            {
                model.Role = RoleList;
                service.Create(model);
                return RedirectToAction("Index", "Admission");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Role = new SelectList(new List<StatusForList> { new StatusForList { Key = "Сотрудник", Display = "Сотрудник" },
                new StatusForList { Key = "Студент", Display = "Студент" } }, "Key", "Display");
            return View(service.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Admission model, string RoleList)
        {
            try
            {
                if (!string.IsNullOrEmpty(RoleList))
                {
                    model.Role = RoleList;
                }
                service.Edit(model);
                return RedirectToAction("Index", "Admission");
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
        public ActionResult Delete(Admission model)
        {
            try
            {
                service.Delete(model);
                return RedirectToAction("Index", "Admission");
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