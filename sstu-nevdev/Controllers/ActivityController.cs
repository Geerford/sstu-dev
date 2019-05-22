using Domain.Core;
using PagedList;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator, SSTU_Inspector, SSTU_Student")]
    public class ActivityController : Controller
    {
        IActivityService service;
        IModeService modeService;
        IIdentityService identityService;
        ICheckpointService checkpointService;

        public ActivityController(IActivityService service, IModeService modeService,
            IIdentityService identityService, ICheckpointService checkpointService)
        {
            this.service = service;
            this.modeService = modeService;
            this.identityService = identityService;
            this.checkpointService = checkpointService;
        }

        public ActionResult Index(int? page)
        {
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(service.GetAll().OrderByDescending(x => x.Date)
                .ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string query, int? page)
        {
            ViewBag.Query = query;
            string[] parsedQuery = query.Split(null);
            List<Activity> result = new List<Activity>();
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
            return View(result.OrderBy(x => x.Date).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var activity = service.Get(id);
            var user = identityService.GetByGUID(activity.IdentityGUID);
            var checkpoint = checkpointService.GetByIP(activity.CheckpointIP);
            if (activity != null && user != null && checkpoint != null)
            {
                return PartialView(new ActivityDetailsViewModel
                {
                    Activity = activity,
                    User = user,
                    Checkpoint = checkpoint
                });
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
            var user = identityService.GetAll().Where(x => x.GUID.Equals(model.IdentityGUID)).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("IdentityGUID", "Данного пользователя не существует");
            }
            var checkpoint = checkpointService.GetAll().Where(x => x.IP.Equals(model.CheckpointIP)).FirstOrDefault();
            if (checkpoint == null)
            {
                ModelState.AddModelError("CheckpointIP", "Данного датчика не существует");
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityViewModel model = (ActivityViewModel)service.Get(id);
            if (model != null)
            {
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
            return HttpNotFound();
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
            var user = identityService.GetAll().Where(x => x.GUID.Equals(model.IdentityGUID)).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("IdentityGUID", "Данного пользователя не существует");
            }
            var checkpoint = checkpointService.GetAll().Where(x => x.IP.Equals(model.CheckpointIP)).FirstOrDefault();
            if (checkpoint == null)
            {
                ModelState.AddModelError("CheckpointIP", "Данного датчика не существует");
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var activity = service.Get(id);
            var user = identityService.GetByGUID(activity.IdentityGUID);
            var checkpoint = checkpointService.GetByIP(activity.CheckpointIP);
            if (activity == null || user == null || checkpoint == null)
            {
                return HttpNotFound();
            }
            return View(new ActivityDetailsViewModel
            {
                Activity = activity,
                User = user,
                Checkpoint = checkpoint
            });
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
            modeService.Dispose();
            identityService.Dispose();
            checkpointService.Dispose();
            base.Dispose(disposing);
        }
    }
}