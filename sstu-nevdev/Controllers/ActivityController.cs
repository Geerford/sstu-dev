using Domain.Core;
using Service.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    public class ActivityController : Controller
    {
        IActivityService service;

        public ActivityController(IActivityService service)
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
            ViewBag.Status = new SelectList(new List<string> { "Успех", "Неудача" }, "Key", "Display");
            ViewBag.Mode = new SelectList(new List<string> { "Вход", "Выход" }, "Key", "Display");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Activity model, string StatusList, string ModeList)
        {
            try
            {
                if (StatusList.Equals("Успех"))
                {
                    model.Status = true;
                }
                else
                {
                    model.Status = false;
                }
                model.Mode = ModeList;

                service.Create(model);
                return RedirectToAction("Index", "Activity");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Status = new SelectList(new List<string> { "Успех", "Неудача" }, "Key", "Display");
            ViewBag.Mode = new SelectList(new List<string> { "Вход", "Выход" }, "Key", "Display");
            return View(service.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Activity model, string StatusList, string ModeList)
        {
            try
            {
                if (!string.IsNullOrEmpty(StatusList))
                {
                    if (StatusList.Equals("Успех"))
                    {
                        model.Status = true;
                    }
                    else
                    {
                        model.Status = false;
                    }
                }
                if (!string.IsNullOrEmpty(ModeList))
                {
                    model.Mode = ModeList;
                }
                service.Edit(model);
                return RedirectToAction("Index", "Activity");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(service.Get(id));
        }

        [HttpPost]
        public ActionResult Delete(Activity model)
        {
            try
            {
                service.Delete(model);
                return RedirectToAction("Index", "Activity");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }
    }
}