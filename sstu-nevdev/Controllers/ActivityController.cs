using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator, SSTU_Inspector, SSTU_Student")]
    public class ActivityController : Controller
    {
        IActivityService service;
        IModeService modeService;

        public ActivityController(IActivityService service, IModeService modeService)
        {
            this.service = service;
            this.modeService = modeService;
        }

        public ActionResult Index()
        {
            return View(service.GetAll());
        }

        public ActionResult Details(int id)
        {
            var item = service.Get(id);
            if (item != null)
            {
                return PartialView(item);
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "SSTU_Administrator")]
        public ActionResult Create()
        {
            ActivityViewModel model = new ActivityViewModel
            {
                StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Успех",
                        Display = "Успех" },
                    new StatusForList {
                        Key = "Неудача",
                        Display = "Неудача" } },
                    "Key", "Display"),
                ModeList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Вход",
                        Display = "Вход" },
                    new StatusForList {
                        Key = "Выход",
                        Display = "Выход" } },
                    "Key", "Display")
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SSTU_Administrator")]
        public ActionResult Create(ActivityViewModel model)
        {
            if (string.IsNullOrEmpty(model.IdentityGUID))
            {
                ModelState.AddModelError("IdentityGUID", "GUID пользователя должен быть заполнен");
            }
            if (string.IsNullOrEmpty(model.CheckpointIP))
            {
                ModelState.AddModelError("CheckpointIP", "IP должен быть заполнен");
            }
            if (!model.Date.HasValue)
            {
                ModelState.AddModelError("Date", "Выберите дату");
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                ModelState.AddModelError("Status", "Выберите статус");
            }
            if (string.IsNullOrEmpty(model.Mode))
            {
                ModelState.AddModelError("Mode", "Выберите режим");
            }
            if (ModelState.IsValid)
            {
                bool status;
                if (model.Status.Equals("Успех"))
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                service.Create(new Activity
                {
                    IdentityGUID = model.IdentityGUID,
                    CheckpointIP = model.CheckpointIP,
                    Date = (System.DateTime)model.Date,
                    ModeID = modeService.GetByMode(model.Mode).FirstOrDefault().ID,
                    Status = status
                });
                return RedirectToAction("Index", "Activity");
            }
            else
            {
                model = new ActivityViewModel
                {
                    StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Успех",
                        Display = "Успех" },
                    new StatusForList {
                        Key = "Неудача",
                        Display = "Неудача" } },
                    "Key", "Display"),
                    ModeList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Вход",
                        Display = "Вход" },
                    new StatusForList {
                        Key = "Выход",
                        Display = "Выход" } },
                    "Key", "Display")
                };
                return View(model);
            }
        }

        [Authorize(Roles = "SSTU_Administrator")]
        public ActionResult Edit(int id)
        {
            ActivityViewModel model = (ActivityViewModel)service.Get(id);
            model.StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Успех",
                        Display = "Успех" },
                    new StatusForList {
                        Key = "Неудача",
                        Display = "Неудача" } },
                    "Key", "Display");
            model.ModeList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Вход",
                        Display = "Вход" },
                    new StatusForList {
                        Key = "Выход",
                        Display = "Выход" } },
                    "Key", "Display");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SSTU_Administrator")]
        public ActionResult Edit(ActivityViewModel model)
        {
            if (string.IsNullOrEmpty(model.IdentityGUID))
            {
                ModelState.AddModelError("IdentityGUID", "GUID пользователя должен быть заполнен");
            }
            if (string.IsNullOrEmpty(model.CheckpointIP))
            {
                ModelState.AddModelError("CheckpointIP", "IP должен быть заполнен");
            }
            if (!model.Date.HasValue)
            {
                ModelState.AddModelError("Date", "Выберите дату");
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                ModelState.AddModelError("Status", "Выберите статус");
            }
            if (string.IsNullOrEmpty(model.Mode))
            {
                ModelState.AddModelError("Mode", "Выберите режим");
            }
            if (ModelState.IsValid)
            {
                bool status;
                if (model.Status.Equals("Успех"))
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                service.Edit(new Activity
                {
                    ID = model.ID,
                    IdentityGUID = model.IdentityGUID,
                    CheckpointIP = model.CheckpointIP,
                    Date = (System.DateTime)model.Date,
                    ModeID = modeService.GetByMode(model.Mode).First().ID,
                    Status = status
                });
                return RedirectToAction("Index", "Activity");
            }
            else
            {
                model = new ActivityViewModel
                {
                    StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Успех",
                        Display = "Успех" },
                    new StatusForList {
                        Key = "Неудача",
                        Display = "Неудача" } },
                    "Key", "Display"),
                    ModeList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Вход",
                        Display = "Вход" },
                    new StatusForList {
                        Key = "Выход",
                        Display = "Выход" } },
                    "Key", "Display")
                };
                return View(model);
            }
        }

        [Authorize(Roles = "SSTU_Administrator")]
        public ActionResult Delete(int id)
        {
            return View(service.Get(id));
        }

        [HttpPost]
        [Authorize(Roles = "SSTU_Administrator")]
        public ActionResult Delete(Activity model)
        {
            try
            {
                service.Delete(model);
                return RedirectToAction("Index", "Activity");
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