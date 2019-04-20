using Service.Interfaces;
using sstu_nevdev.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace sstu_nevdev.Controllers
{
    public class DatabaseController : Controller
    {
        IDatabaseService service;

        public DatabaseController(IDatabaseService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            DatabaseViewModel model = new DatabaseViewModel
            {
                Backups = Directory.GetFiles(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "*.bak")
                                .Select(x => new KeyValuePair<string, DateTime>(Path.GetFileName(x), System.IO.File.GetCreationTime(x)))
                                     .ToDictionary(x => x.Key, x => x.Value)
            };
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(DatabaseViewModel model)
        {
            if(model == null)
            {
                model = new DatabaseViewModel();
            }
            else
            {
                model.Backups = new Dictionary<string, DateTime>();
            }
            model.Backups = Directory.GetFiles(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "*.bak")
                                .Select(x => new KeyValuePair<string, DateTime>(Path.GetFileName(x), System.IO.File.GetCreationTime(x)))
                                     .ToDictionary(x => x.Key, x => x.Value);
            return View("Index", model);
        }

        public ActionResult Backup()
        {
            string backupName = service.Backup();
            if (!string.IsNullOrEmpty(backupName))
            {
                return Index(new DatabaseViewModel { Backup = backupName });
            }
            return Index(null);
        }

        public ActionResult Recovery(string backupName)
        {
            bool isExist = System.IO.File.Exists(AppDomain.CurrentDomain.GetData("DataDirectory")
                            .ToString() + "\\" + backupName);
            if (isExist)
            {
                service.Recovery(backupName);
                return Index(new DatabaseViewModel { Status = true });
            }
            return Index(new DatabaseViewModel { Status = false });
        }
    }
}