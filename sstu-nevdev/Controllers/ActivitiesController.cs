using Domain.Core;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace sstu_nevdev.Controllers
{
    public class ActivitiesController : ApiController
    {
        IActivityService activityService;
        ICheckpointService checkpointService;
        IIdentityService identityService;
        ITypeService typeService;

        public ActivitiesController(IActivityService activityService, ICheckpointService checkpointService, 
            IIdentityService identityService, ITypeService typeService)
        {
            this.activityService = activityService;
            this.checkpointService = checkpointService;
            this.identityService = identityService;
            this.typeService = typeService;
        }

        // GET api/activities
        public IEnumerable<Activity> Get()
        {
            return activityService.GetAll();
        }

        // GET api/activities/5
        public Activity Get(int id)
        {
            return activityService.Get(id);
        }

        // POST api/activities
        [HttpPost]
        public IHttpActionResult Post([FromBody]ActivityModel model)
        {
            Checkpoint checkpoint = checkpointService.Get(model.CheckpointID);
            Identity identity;
            if (model.RFID.Length != 0)
            {
                identity = identityService.GetByRFID(model.RFID);
            }
            else
            {
                identity = identityService.GetByQR(model.QR);
            }
            if(identity != null)
            {
                string picturePath = "/Content/uploads/" + identity.Picture;
                Type checkpointType = typeService.Get(checkpoint.TypeID);
                switch (checkpointType.Status)
                {
                    case "Пропускной":
                        if (activityService.IsPassed(identity.ID))
                        {
                            activityService.Create(new Activity
                            {
                                CheckpointID = checkpoint.ID,
                                CreatedBy = "Checkpoint #" + checkpoint.ID,
                                Date = System.DateTime.Now,
                                IdentityID = identity.ID,
                                Mode = "Выход",
                                Status = true
                            });
                            return Ok();
                        }
                        else
                        {
                            if (activityService.IsAdmission(checkpoint.ID, identity.RoleID))
                            {
                                activityService.Create(new Activity
                                {
                                    CheckpointID = checkpoint.ID,
                                    CreatedBy = "Checkpoint #" + checkpoint.ID,
                                    Date = System.DateTime.Now,
                                    IdentityID = identity.ID,
                                    Mode = "Вход",
                                    Status = true
                                });
                                var test = new IdentityResponseModel
                                {
                                    Birthdate = identity.Birthdate,
                                    City = identity.City,
                                    Country = identity.Country,
                                    CreatedBy = identity.CreatedBy,
                                    Department = identity.Department,
                                    ID = identity.ID,
                                    Email = identity.Email,
                                    Gender = identity.Gender,
                                    Group = identity.Group,
                                    RFID = identity.RFID,
                                    Midname = identity.Midname,
                                    Name = identity.Name,
                                    Phone = identity.Phone,
                                    Picture = picturePath,
                                    QR = identity.QR,
                                    Role = identity.Role.Description,
                                    Status = identity.Status,
                                    Surname = identity.Surname,
                                    UpdatedBy = identity.UpdatedBy
                                };
                                var response = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(test);
                                return Json(response);
                            }
                            return Json("Permission denied");
                        }
                    case "Лекционный":
                        if (activityService.IsPassed(identity.ID))
                        {
                            activityService.Create(new Activity
                            {
                                CheckpointID = checkpoint.ID,
                                CreatedBy = "Checkpoint #" + checkpoint.ID,
                                Date = System.DateTime.Now,
                                IdentityID = identity.ID,
                                Mode = "Выход",
                                Status = true
                            });
                            return Ok();
                        }
                        else
                        {
                            activityService.Create(new Activity
                            {
                                CheckpointID = checkpoint.ID,
                                CreatedBy = "Checkpoint #" + checkpoint.ID,
                                Date = System.DateTime.Now,
                                IdentityID = identity.ID,
                                Mode = "Вход",
                                Status = true
                            });
                            return Ok();
                        }
                    default:
                        activityService.Create(new Activity
                        {
                            CheckpointID = checkpoint.ID,
                            CreatedBy = "Checkpoint #" + checkpoint.ID,
                            Date = System.DateTime.Now,
                            IdentityID = identity.ID,
                            Mode = "Проход",
                            Status = true
                        });
                        return Ok();
                }
            }
            return Json("Object does not exist");
        }

        // PUT api/activities/5
        [HttpPut]
        public void Put(int id, [FromBody]Activity item)
        {
            if (id == item.ID)
            {
                activityService.Edit(item);
            }
        }

        // DELETE api/activities/5
        [HttpDelete]
        public void Delete(int id)
        {
            Activity item = activityService.Get(id);
            if (item != null)
            {
                activityService.Delete(item);
            }
        }
    }
}