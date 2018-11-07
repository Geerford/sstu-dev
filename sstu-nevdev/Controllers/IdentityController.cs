using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using static sstu_nevdev.Models.IdentityModel;

namespace sstu_nevdev.Controllers
{
    public class IdentityController : Controller
    {
        IIdentityService identityService;
        IRoleService roleService;

        public IdentityController(IIdentityService identityService, IRoleService roleService)
        {
            this.identityService = identityService;
            this.roleService = roleService;
        }

        public ActionResult Index()
        {
            return View(identityService.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View(identityService.Get(id));
        }

        public ActionResult Create()
        {
            List<StatusForList> roles = new List<StatusForList>();
            foreach (Role item in roleService.GetAll())
            {
                roles.Add(new StatusForList
                {
                    Key = item.ID.ToString(),
                    Display = item.Description
                });
            }

            return View(new IdentityModel
            {
                Role = new SelectList(roles, "Key", "Display")
            });
        }

        [HttpPost]
        public ActionResult Create(IdentityModel model, string RoleList)
        {
            try
            {
                identityService.Create(new Identity
                {
                    RFID = model.RFID,
                    QR = model.QR,
                    Name = model.Name,
                    Surname = model.Surname,
                    Midname = model.Midname,
                    Gender = model.Gender,
                    Birthdate = model.Birthdate,
                    Picture = model.Picture,
                    Country = model.Country,
                    City = model.City,
                    Phone = model.Phone,
                    Email = model.Email,
                    Department = model.Department,
                    Group = model.Group,
                    Status = model.Status,
                    CreatedBy = model.CreatedBy,
                    UpdatedBy = model.UpdatedBy,
                    RoleID = System.Convert.ToInt32(RoleList)
                });
                return RedirectToAction("Index", "Identity", new { });
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            List<StatusForList> roles = new List<StatusForList>();
            foreach (Role item in roleService.GetAll())
            {
                roles.Add(new StatusForList
                {
                    Key = item.ID.ToString(),
                    Display = item.Description
                });
            }

            Identity model = identityService.Get(id);

            return View(new IdentityModel
            {
                RFID = model.RFID,
                QR = model.QR,
                Name = model.Name,
                Surname = model.Surname,
                Midname = model.Midname,
                Gender = model.Gender,
                Birthdate = model.Birthdate,
                Picture = model.Picture,
                Country = model.Country,
                City = model.City,
                Phone = model.Phone,
                Email = model.Email,
                Department = model.Department,
                Group = model.Group,
                Status = model.Status,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.UpdatedBy,
                Role = new SelectList(roles, "Key", "Display")
            });
        }

        [HttpPost]
        public ActionResult Edit(IdentityModel model, string RoleList)
        {
            try
            {
                Identity identity = identityService.Get(model.ID);
                if (model.RFID != null)
                {
                    identity.RFID = model.RFID;
                }
                if (model.QR != null)
                {
                    identity.QR = model.QR;
                }
                if (model.Name != null)
                {
                    identity.Name = model.Name;
                }
                if (model.Surname != null)
                {
                    identity.Surname = model.Surname;
                }
                if (model.Midname != null)
                {
                    identity.Midname = model.Midname;
                }
                if (model.Gender != false)
                {
                    identity.Gender = model.Gender;
                }
                if (model.Birthdate != null)
                {
                    identity.Birthdate = model.Birthdate;
                }
                if (model.Picture != null)
                {
                    identity.Picture = model.Picture;
                }
                if (model.Country != null)
                {
                    identity.Country = model.Country;
                }
                if (model.City != null)
                {
                    identity.City = model.City;
                }
                if (model.Phone != null)
                {
                    identity.Phone = model.Phone;
                }
                if (model.Email != null)
                {
                    identity.Email = model.Email;
                }
                if (model.Department != null)
                {
                    identity.Department = model.Department;
                }
                if (model.Group != null)
                {
                    identity.Group = model.Group;
                }
                if (model.Status != null)
                {
                    identity.Status = model.Status;
                }
                if (model.CreatedBy != null)
                {
                    identity.CreatedBy = model.CreatedBy;
                }
                if (model.UpdatedBy != null)
                {
                    identity.UpdatedBy = model.UpdatedBy;
                }
                if (RoleList != null)
                {
                    identity.RoleID = System.Convert.ToInt32(RoleList);
                }

                identityService.Edit(identity);

                return RedirectToAction("Index", "Identity", new { });
            }
            catch (System.Exception ex)
            {
                return View(ex);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(identityService.Get(id));
        }

        [HttpPost]
        public ActionResult Delete(Identity model)
        {
            try
            {
                identityService.Delete(model);
                return RedirectToAction("Index", "Identity", new { });
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            identityService.Dispose();
            roleService.Dispose();
            base.Dispose(disposing);
        }
    }
}