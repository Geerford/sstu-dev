using Domain.Core;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services.Business.Services
{
    public class ActivityService : IActivityService
    {
        IUnitOfWork Database { get; set; }

        public ActivityService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Activity model)
        {
            Database.Activity.Create(model);
            Database.Save();
        }

        public void Delete(Activity model)
        {
            Database.Activity.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Activity model)
        {
            Database.Activity.Update(model);
            Database.Save();
        }

        public Activity Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Activity item = Database.Activity.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Activity> GetAll()
        {
            return Database.Activity.GetAll().ToList();
        }

        public IEnumerable<Activity> GetByStatus(bool status)
        {
            return Database.Activity.Find(x => x.Status == status);
        }

        public bool IsAdmission(int? checkpointID, string role)
        {
            if (checkpointID == null || string.IsNullOrEmpty(role))
            {
                throw new ValidationException("Не задананы параметры", "");
            }
            foreach (CheckpointAdmission item in Database.CheckpointAdmission.GetAll().Where(x => x.CheckpointID == checkpointID))
            {
                Admission admission = Database.Admission.Get(item.AdmissionID);
                if (admission.Role.Equals(role))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsPassed(string IdentityGUID)
        {
            if (string.IsNullOrEmpty(IdentityGUID))
            {
                throw new ValidationException("Не задан ID", "");
            }
            Activity activity = Database.Activity.GetAll().Where(x => x.IdentityGUID == IdentityGUID).FirstOrDefault();
            if (activity != null && activity.Mode == "Вход")
            {
                return true; //Person in the room
            }
            return false;
        }

        public int IsOk(CheckpointDTO checkpoint, IdentityDTO identity)
        {
            Type checkpointType = Database.Type.Get(checkpoint.Type.ID);
            /* Codes:
              -1 -- Ok. No response
              200 -- Ok. Return response
              500 -- Fail. Permission denied */
            switch (checkpointType.Status)
            {
                case "Пропускной":
                    if (IsPassed(identity.GUID))
                    {
                        Create(new Activity
                        {
                            CheckpointIP = checkpoint.IP,
                            Date = System.DateTime.Now,
                            IdentityGUID = identity.GUID,
                            Mode = "Выход",
                            Status = true
                        });
                        return -1; 
                    }
                    else
                    {
                        if (IsAdmission(checkpoint.ID, identity.Role))
                        {
                            Create(new Activity
                            {
                                CheckpointIP = checkpoint.IP,
                                Date = System.DateTime.Now,
                                IdentityGUID = identity.GUID,
                                Mode = "Вход",
                                Status = true
                            });
                            return 200; 
                        }
                        return 500;
                    }
                case "Лекционный":
                    if (IsPassed(identity.GUID))
                    {
                        Create(new Activity
                        {
                            CheckpointIP = checkpoint.IP,
                            Date = System.DateTime.Now,
                            IdentityGUID = identity.GUID,
                            Mode = "Выход",
                            Status = true
                        });
                        return -1;
                    }
                    else
                    {
                        Create(new Activity
                        {
                            CheckpointIP = checkpoint.IP,
                            Date = System.DateTime.Now,
                            IdentityGUID = identity.GUID,
                            Mode = "Вход",
                            Status = true
                        });
                        return -1;
                    }
                default:
                    Create(new Activity
                    {
                        CheckpointIP = checkpoint.IP,
                        Date = System.DateTime.Now,
                        IdentityGUID = identity.GUID,
                        Mode = "Проход",
                        Status = true
                    });
                    return -1;
            }
        }
    }
}