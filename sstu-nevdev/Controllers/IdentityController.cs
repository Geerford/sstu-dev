using Domain.Core;
using Service.Interfaces;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Identity model, HttpPostedFileBase filedata = null)
        {
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/uploads/"), fileName = "";
                if (filedata != null)
                {
                    string fileEx = Path.GetExtension(filedata.FileName);
                    fileName = System.Guid.NewGuid().ToString() + fileEx;
                    filedata.SaveAs(path + fileName);
                }
                model.Picture = fileName;
                service.Create(model);
                return RedirectToAction("Index", "Identity");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            return View(service.GetSimple(id));
        }

        [HttpPost]
        public ActionResult Edit(Identity model, HttpPostedFileBase filedata = null)
        {
            try
            {
                Identity result = service.GetSimple(model.ID);
                if (model.GUID != null)
                {
                    result.GUID = model.GUID;
                }
                if (model.RFID != null)
                {
                    result.RFID = model.RFID;
                }
                if (model.QR != null)
                {
                    result.QR = model.QR;
                }
                if (filedata != null)
                {
                    result.Picture = service.SaveImage(filedata);
                }
                service.Edit(result);

                return RedirectToAction("Index", "Identity");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(service.GetSimple(id));
        }

        [HttpPost]
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