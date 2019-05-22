using Domain.Core;
using PagedList;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator, SSTU_Inspector")]
    public class CheckpointController : Controller
    {
        ICheckpointService checkpointService;
        ITypeService typeService;
        IAdmissionService admissionService;

        public CheckpointController(ICheckpointService checkpointService, ITypeService typeService, 
            IAdmissionService admissionService)
        {
            this.checkpointService = checkpointService;
            this.typeService = typeService;
            this.admissionService = admissionService;
        }

        public ActionResult Index(int? page)
        {
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(checkpointService.GetAll().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string query, int? page)
        {
            ViewBag.Query = query;
            string[] parsedQuery = query.Split(null);
            List<CheckpointDTO> result = new List<CheckpointDTO>();
            foreach (var item in checkpointService.GetAll())
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
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = checkpointService.Get(id);
            if (item != null)
            {
                return PartialView(item);
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "SSTU_Administrator")]
        public ActionResult Create()
        {
            List<StatusForList> types = new List<StatusForList>();
            foreach (Type item in typeService.GetAll())
            {
                types.Add(new StatusForList
                {
                    Key = item.ID.ToString(),
                    Display = item.Description
                });
            }
            List<CheckboxItem> admissions = new List<CheckboxItem>();
            foreach (var item in admissionService.GetAll())
            {
                string display = item.Role + " (" + item.Description + ")";
                admissions.Add(new CheckboxItem()
                {
                    ID = item.ID,
                    Display = display,
                    IsChecked = false
                });
            }
            CheckpointViewModel model = new CheckpointViewModel
            {
                StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Пропуск",
                        Display = "Пропуск" },
                    new StatusForList {
                        Key = "Отметка",
                        Display = "Отметка" } },
                    "Key", "Display"),
                TypeList = new SelectList(types, "Key", "Display"),
                AdmissionList = admissions
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SSTU_Administrator")]
        public ActionResult Create(CheckpointViewModel model)
        {
            if (string.IsNullOrEmpty(model.IP))
            {
                ModelState.AddModelError("IP", "IP должен быть заполнен");
            }
            if (model.Campus == null)
            {
                ModelState.AddModelError("Campus", "Корпус должен быть заполнен");
            }
            if (model.Row == null)
            {
                ModelState.AddModelError("Row", "Этаж должен быть заполнен");
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                ModelState.AddModelError("Description", "Описание должно быть заполнено");
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                ModelState.AddModelError("Status", "Выберите статус");
            }
            if (string.IsNullOrEmpty(model.TypeID))
            {
                ModelState.AddModelError("TypeID", "Выберите тип");
            }
            if (ModelState.IsValid)
            {
                List<Admission> admissions = new List<Admission>();
                foreach (var item in model.AdmissionList)
                {
                    if (item.IsChecked)
                    {
                        admissions.Add(admissionService.Get(item.ID));
                    }
                }
                model.Admissions = admissions;
                model.Type = (TypeDTO)typeService.Get(System.Convert.ToInt32(model.TypeID));
                checkpointService.Create(new CheckpointDTO
                {
                    IP = model.IP,
                    Campus = (int)model.Campus,
                    Row = (int)model.Row,
                    Classroom = model.Classroom,
                    Section = model.Section,
                    Description = model.Description,
                    Status = model.Status,
                    Type = model.Type,
                    Admissions = model.Admissions
                });
                return RedirectToAction("Index", "Checkpoint");
            }
            else
            {
                List<StatusForList> types = new List<StatusForList>();
                foreach (Type item in typeService.GetAll())
                {
                    types.Add(new StatusForList
                    {
                        Key = item.ID.ToString(),
                        Display = item.Description
                    });
                }
                List<CheckboxItem> admissions = new List<CheckboxItem>();
                foreach (var item in admissionService.GetAll())
                {
                    string display = item.Role + " (" + item.Description + ")";
                    admissions.Add(new CheckboxItem()
                    {
                        ID = item.ID,
                        Display = display,
                        IsChecked = false
                    });
                }
                model.StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Пропуск",
                        Display = "Пропуск" },
                    new StatusForList {
                        Key = "Отметка",
                        Display = "Отметка" } },
                        "Key", "Display");
                model.TypeList = new SelectList(types, "Key", "Display");
                model.AdmissionList = admissions;
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
            List<StatusForList> types = new List<StatusForList>();
            foreach (Type item in typeService.GetAll())
            {
                types.Add(new StatusForList
                {
                    Key = item.ID.ToString(),
                    Display = item.Description
                });
            }
            List<CheckboxItem> admissions = new List<CheckboxItem>();
            foreach (var item in admissionService.GetAll())
            {
                string display = item.Role + " (" + item.Description + ")";
                if (checkpointService.IsMatchAdmission(id, item.ID))
                {
                    admissions.Add(new CheckboxItem()
                    {
                        ID = item.ID,
                        Display = display,
                        IsChecked = true
                    });
                }
                else
                {
                    admissions.Add(new CheckboxItem()
                    {
                        ID = item.ID,
                        Display = display,
                        IsChecked = false
                    });
                }
            }
            CheckpointViewModel model = (CheckpointViewModel)checkpointService.Get(id);
            if (model != null)
            {
                model.StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Пропуск",
                        Display = "Пропуск" },
                    new StatusForList {
                        Key = "Отметка",
                        Display = "Отметка" } }, "Key", "Display");
                model.TypeList = new SelectList(types, "Key", "Display");
                model.AdmissionList = admissions;
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SSTU_Administrator")]
        public ActionResult Edit(CheckpointViewModel model)
        {
            if (string.IsNullOrEmpty(model.IP))
            {
                ModelState.AddModelError("IP", "IP должен быть заполнен");
            }
            if (model.Campus == null)
            {
                ModelState.AddModelError("Campus", "Корпус должен быть заполнен");
            }
            if (model.Row == null)
            {
                ModelState.AddModelError("Row", "Этаж должен быть заполнен");
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                ModelState.AddModelError("Description", "Описание должно быть заполнено");
            }
            if (string.IsNullOrEmpty(model.Status))
            {
                ModelState.AddModelError("Status", "Выберите статус");
            }
            if (string.IsNullOrEmpty(model.TypeID))
            {
                ModelState.AddModelError("TypeID", "Выберите тип");
            }
            if (ModelState.IsValid)
            {
                List<Admission> admissions = new List<Admission>();
                foreach (var item in model.AdmissionList)
                {
                    if (item.IsChecked)
                    {
                        admissions.Add(admissionService.Get(item.ID));
                    }
                }
                model.Admissions = admissions;
                model.Type = (TypeDTO)typeService.Get(System.Convert.ToInt32(model.TypeID));
                checkpointService.Edit(new CheckpointDTO
                {
                    ID = model.ID,
                    IP = model.IP,
                    Campus = (int)model.Campus,
                    Classroom = model.Classroom,
                    Section = model.Section,
                    Row = (int)model.Row,
                    Description = model.Description,
                    Status = model.Status,
                    Type = model.Type,
                    Admissions = model.Admissions
                });
                return RedirectToAction("Index", "Checkpoint");
            }
            else
            {
                List<StatusForList> types = new List<StatusForList>();
                foreach (Type item in typeService.GetAll())
                {
                    types.Add(new StatusForList
                    {
                        Key = item.ID.ToString(),
                        Display = item.Description
                    });
                }
                List<CheckboxItem> admissions = new List<CheckboxItem>();
                foreach (var item in admissionService.GetAll())
                {
                    string display = item.Role + " (" + item.Description + ")";
                    if (checkpointService.IsMatchAdmission(model.ID, item.ID))
                    {
                        admissions.Add(new CheckboxItem()
                        {
                            ID = item.ID,
                            Display = display,
                            IsChecked = true
                        });
                    }
                    else
                    {
                        admissions.Add(new CheckboxItem()
                        {
                            ID = item.ID,
                            Display = display,
                            IsChecked = false
                        });
                    }
                }
                model = new CheckpointViewModel
                {
                    ID = model.ID,
                    StatusList = new SelectList(new List<StatusForList> {
                    new StatusForList {
                        Key = "Пропуск",
                        Display = "Пропуск" },
                    new StatusForList {
                        Key = "Отметка",
                        Display = "Отметка" } },
                        "Key", "Display"),
                    TypeList = new SelectList(types, "Key", "Display"),
                    AdmissionList = admissions
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
            CheckpointDTO model = checkpointService.Get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SSTU_Administrator")]
        public ActionResult Delete(CheckpointDTO model)
        {
            try
            {
                checkpointService.DeleteAllAdmission(model.ID);
                checkpointService.Delete(checkpointService.GetSimple(model.ID));
                return RedirectToAction("Index", "Checkpoint");
            }
            catch
            {
                return View(model);
            }
        }

        protected override void Dispose(bool disposing)
        {
            checkpointService.Dispose();
            typeService.Dispose();
            admissionService.Dispose();
            base.Dispose(disposing);
        }
    }
}