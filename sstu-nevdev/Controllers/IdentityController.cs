using Domain.Core;
using PagedList;
using Service.DTO;
using Service.Interfaces;
using sstu_nevdev.App_Start;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator, SSTU_Inspector")]
    public class IdentityController : Controller
    {
        IIdentityService service;
        IActivityService activityService;

        public IdentityController(IIdentityService service, IActivityService activityService)
        {
            this.service = service;
            this.activityService = activityService;
        }

        public ActionResult Index(int? page)
        {
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(service.GetAll().OrderBy(x => x.GUID).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string query, int? page)
        {
            ViewBag.Query = query;
            string[] parsedQuery = query.Split(null);
            List<IdentityDTO> result = new List<IdentityDTO>();
            foreach (var item in service.GetAll())
            {
                foreach(var value in parsedQuery)
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
            return View(result.OrderBy(x => x.GUID).ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = service.Get(id);
            var activities = activityService.GetAll().Where(a => a.IdentityGUID.Equals(user.GUID));
            if (user != null)
            {
                return PartialView(new IdentityDetailsViewModel
                {
                    User = user,
                    Activities = activities
                });
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = service.GetSimple(id);
            if (user != null)
            {
                return View(new IdentityViewModel(user));
            }
            return HttpNotFound();
        }

        public ActionResult Users(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(service.GetAll().Where(g => !g.GUID.Contains("GUEST") && g.Picture == null)
                .OrderBy(x => x.GUID).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Create(IdentityViewModel model, HttpPostedFileBase filedata = null)
        {
            if (ModelState.IsValid && filedata != null)
            {
                var user = service.GetSimple(model.ID);
                user.Picture = service.SaveImage(filedata, null);
                service.Edit(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityViewModel model = (IdentityViewModel)service.GetSimple(id);
            if (model != null)
            {
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Edit(IdentityViewModel model, HttpPostedFileBase filedata = null)
        {           
            if (ModelState.IsValid && filedata != null)
            {
                var user = service.GetSimple(model.ID);
                user.Picture = service.SaveImage(filedata, user.Picture);
                service.Edit(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Identity model = service.GetSimple(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SSTU_Deanery, SSTU_Administrator")]
        public ActionResult Delete(Identity model)
        {
            try
            {
                service.SaveImage(null, model.Picture);
                model.Picture = null;
                service.Edit(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            activityService.Dispose();
            base.Dispose(disposing);
        }
    }
}