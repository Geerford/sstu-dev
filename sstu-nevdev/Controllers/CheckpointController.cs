using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static sstu_nevdev.Models.CheckpointModel;

namespace sstu_nevdev.Controllers
{
    public class CheckpointController : Controller
    {
        ICheckpointService checkpointService;
        ITypeService typeService;
        IAdmissionService admissionService;
        ICheckpointAdmissionService admissionCheckpointService;

        public CheckpointController(ICheckpointService checkpointService, ITypeService typeService, IAdmissionService admissionService, ICheckpointAdmissionService admissionCheckpointService)
        {
            this.checkpointService = checkpointService;
            this.typeService = typeService;
            this.admissionService = admissionService;
            this.admissionCheckpointService = admissionCheckpointService;
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
                checkpointService.Create(new Checkpoint {
                    Campus = model.Campus,
                    CreatedBy = model.CreatedBy,
                    Description = model.Description,
                    Row = model.Row,
                    Status = model.Status,
                    TypeID = System.Convert.ToInt32(TypeList),
                    UpdatedBy = model.UpdatedBy
                });
                int checkpointID = checkpointService.GetAll().Where(d => d.Description == model.Description).Last().ID;
                foreach (var item in model.Admissions)
                {
                    if (item.IsChecked)
                    {
                        admissionCheckpointService.Create(new CheckpointAdmission { AdmissionID = item.ID, CheckpointID = checkpointID });
                    }
                }
                return RedirectToAction("Index", "Checkpoint", new { });
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
                if (admissionCheckpointService.IsMatch(id, item.ID))
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
            Checkpoint model = checkpointService.Get(id);

            return View(new CheckpointModel
            {
                Campus = model.Campus,
                CreatedBy = model.CreatedBy,
                Description = model.Description,
                Row = model.Row,
                Status = model.Status,
                UpdatedBy = model.UpdatedBy,
                Admissions = admissions,
                Type = new SelectList(types, "Key", "Display")
            });
        }

        [HttpPost]
        public ActionResult Edit(CheckpointModel model, string TypeList)
        {
            try
            {
                Checkpoint checkpoint = checkpointService.Get(model.ID); 
                if(model.Row != 0)
                {
                    checkpoint.Row = model.Row;
                }
                if (model.Status != null)
                {
                    checkpoint.Status = model.Status;
                }
                if (model.Campus != 0)
                {
                    checkpoint.Campus = model.Campus;
                }
                if (model.CreatedBy != null)
                {
                    checkpoint.CreatedBy = model.CreatedBy;
                }
                if (model.Description != null)
                {
                    checkpoint.Description = model.Description;
                }
                if (TypeList != null)
                {
                    checkpoint.TypeID = System.Convert.ToInt32(TypeList);
                }
                if (model.UpdatedBy != null)
                {
                    checkpoint.UpdatedBy = model.UpdatedBy;
                }


                checkpointService.Edit(checkpoint);

                foreach (var item in model.Admissions)
                {
                    if (item.IsChecked)
                    {
                        if (!admissionCheckpointService.IsMatch(model.ID, item.ID))
                        {
                            admissionCheckpointService.Create(new CheckpointAdmission { AdmissionID = item.ID, CheckpointID = model.ID });
                        }
                    }
                    else
                    {
                        if (admissionCheckpointService.IsMatch(model.ID, item.ID))
                        {

                            CheckpointAdmission tryToDelete = admissionCheckpointService.GetAll().Where(c => (c.CheckpointID == model.ID) && (c.AdmissionID == item.ID)).FirstOrDefault();
                            admissionCheckpointService.Delete(tryToDelete);
                        }
                    }
                }

                return RedirectToAction("Index", "Checkpoint", new { });
            }
            catch(System.Exception ex)
            {
                return View(ex);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(checkpointService.Get(id));
        }

        [HttpPost]
        public ActionResult Delete(Checkpoint model)
        {
            try
            {
                checkpointService.Delete(model);
                return RedirectToAction("Index", "Checkpoint", new { });
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            checkpointService.Dispose();
            typeService.Dispose();
            admissionService.Dispose();
            admissionCheckpointService.Dispose();
            base.Dispose(disposing);
        }
    }
}