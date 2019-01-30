using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator, SSTU_Inspector")]
    public class IdentityController : Controller
    {
        IIdentityService service;

        public IdentityController(IIdentityService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View(service.GetAll());
        }

        public ActionResult OneC()
        {
            return View(service.GetUsers1C());
        }

        public ActionResult Details(int id)
        {
            return View(service.Get(id));
        }

        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Create(IdentityViewModel model, HttpPostedFileBase filedata = null)
        {
            if (string.IsNullOrEmpty(model.GUID))
            {
                ModelState.AddModelError("GUID", "GUID должен быть заполнен");
            }
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (filedata != null)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/uploads/"), fileEx = Path.GetExtension(filedata.FileName);
                    fileName = System.Guid.NewGuid().ToString() + fileEx;
                    filedata.SaveAs(path + fileName);
                }
                model.Picture = fileName;
                service.Create(new Identity
                {
                    GUID = model.GUID,
                    Picture = model.Picture
                });
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Edit(int id)
        {
            return View((IdentityViewModel)service.GetSimple(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Edit(IdentityViewModel model, HttpPostedFileBase filedata = null)
        {
            if (string.IsNullOrEmpty(model.GUID))
            {
                ModelState.AddModelError("GUID", "GUID должен быть заполнен");
            }
            if (ModelState.IsValid)
            {
                Identity result = service.GetSimple(model.ID);

                if (filedata != null)
                {
                    result.Picture = service.SaveImage(filedata);
                }
                service.Edit(result);
                return RedirectToAction("Index", "Identity");
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Delete(int id)
        {
            return View(service.GetSimple(id));
        }

        [HttpPost]
        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Delete(Identity model)
        {
            try
            {
                service.Delete(model);
                return RedirectToAction("Index", "Identity");
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