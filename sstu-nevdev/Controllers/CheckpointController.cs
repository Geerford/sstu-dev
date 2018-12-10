using Domain.Core;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using static sstu_nevdev.Models.CheckpointModel;

namespace sstu_nevdev.Controllers
{
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

        public ActionResult Index()
        {
            return View(checkpointService.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View(checkpointService.Get(id));
        }

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
                admissions.Add(new CheckboxItem()
                {
                    ID = item.ID,
                    Display = item.Description,
                    IsChecked = false
                });
            }

            return View(new CheckpointModel {
                Admissions = admissions,
                Type = new SelectList(types, "Key", "Display")
            });
        }

        [HttpPost]
        public ActionResult Create(CheckpointModel model, string TypeList)
        {
            try
            {
                List<Admission> admissions = new List<Admission>();
                foreach (var item in model.Admissions)
                {
                    if (item.IsChecked)
                    {
                        admissions.Add(admissionService.Get(item.ID));
                    }
                }
                checkpointService.Create(new CheckpointDTO {
                    IP = model.IP,
                    Campus = model.Campus,
                    Description = model.Description,
                    Row = model.Row,
                    Status = model.Status,
                    Type = typeService.Get(System.Convert.ToInt32(TypeList)),
                    Admissions = admissions
                });
                return RedirectToAction("Index", "Checkpoint");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var types = new List<StatusForList>();
            foreach (Type item in typeService.GetAll())
            {
                types.Add(new StatusForList
                {
                    Key = item.ID.ToString(),
                    Display = item.Description
                });
            }

            var admissions = new List<CheckboxItem>();
            foreach (var item in admissionService.GetAll())
            {
                if (checkpointService.IsMatchAdmission(id, item.ID))
                {
                    admissions.Add(new CheckboxItem()
                    {
                        ID = item.ID,
                        Display = item.Description,
                        IsChecked = true
                    });
                }
                else
                {
                    admissions.Add(new CheckboxItem()
                    {
                        ID = item.ID,
                        Display = item.Description,
                        IsChecked = false
                    });
                }
            }
            Checkpoint model = checkpointService.GetSimple(id);
            return View(new CheckpointModel
            {
                IP = model.IP,
                Campus = model.Campus,
                Description = model.Description,
                Row = model.Row,
                Status = model.Status,
                Admissions = admissions,
                Type = new SelectList(types, "Key", "Display")
        });
        }

        [HttpPost]
        public ActionResult Edit(CheckpointModel model, string TypeList)
        {
            try
            {
                List<Admission> admissions = new List<Admission>();
                foreach(var item in model.Admissions)
                {
                    if (item.IsChecked)
                    {
                        admissions.Add(admissionService.Get(item.ID));
                    }
                }
                checkpointService.Edit(new CheckpointDTO
                {
                    ID = model.ID,
                    IP = model.IP,
                    Campus = model.Campus,
                    Description = model.Description,
                    Row = model.Row,
                    Status = model.Status,
                    Type = typeService.Get(System.Convert.ToInt32(TypeList)),
                    Admissions = admissions
                });
                return RedirectToAction("Index", "Checkpoint");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(checkpointService.Get(id));
        }

        [HttpPost]
        public ActionResult Delete(CheckpointDTO model)
        {
            try
            {
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