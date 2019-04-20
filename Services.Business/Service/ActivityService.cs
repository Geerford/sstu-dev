using Domain.Core;
using Repository.Interfaces;
using Service.DTO;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services.Business.Service
{
    /// <summary>
    /// Implements <see cref="IActivityService"/>.
    /// </summary>
    public class ActivityService : IActivityService
    {
        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityService"/> class.
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> object.</param>
        public ActivityService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Implements <see cref="IActivityService.Create(Activity)"/>.
        /// </summary>
        public void Create(Activity model)
        {
            Database.Activity.Create(model);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IActivityService.Delete(Activity)"/>.
        /// </summary>
        public void Delete(Activity model)
        {
            Database.Activity.Delete(model.ID);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IActivityService.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>
        /// Implements <see cref="IActivityService.Edit(Activity)"/>.
        /// </summary>
        public void Edit(Activity model)
        {
            Database.Activity.Update(model);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IActivityService.Get(int?)"/>.
        /// </summary>
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

        /// <summary>
        /// Implements <see cref="IActivityService.GetAll"/>.
        /// </summary>
        public IEnumerable<Activity> GetAll()
        {
            return Database.Activity.GetAll();
        }

        /// <summary>
        /// Implements <see cref="IActivityService.GetByStatus(bool)"/>.
        /// </summary>
        public IEnumerable<Activity> GetByStatus(bool? status)
        {
            if (status == null)
            {
                throw new ValidationException("Не задан статус", "");
            }
            return Database.Activity.Find(x => x.Status == status);
        }

        /// <summary>
        /// Implements <see cref="IActivityService.IsAdmission()"/>.
        /// </summary>
        public bool IsAdmission(int? checkpointID, string role)
        {
            if (checkpointID == null || string.IsNullOrEmpty(role))
            {
                throw new ValidationException("Не задананы параметры", "");
            }
            var admissions = Database.Admission.GetAll();
            foreach (CheckpointAdmission item in Database.CheckpointAdmission.GetAll().Where(x => x.CheckpointID == checkpointID))
            {
                Admission admission = admissions.Where(y => y.ID == item.AdmissionID).FirstOrDefault();
                if (admission.Role.Equals(role))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Implements <see cref="IActivityService.IsOk(CheckpointDTO, IdentityDTO)()"/>.
        /// </summary>
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
                            ModeID = 2,
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
                                ModeID = 1,
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
                            ModeID = 2,
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
                            ModeID = 1,
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
                        ModeID = 3,
                        Status = true
                    });
                    return -1;
            }
        }

        /// <summary>
        /// Implements <see cref="IActivityService.IsPassed(string)()"/>.
        /// </summary>
        public bool IsPassed(string identityGUID)
        {
            if (string.IsNullOrEmpty(identityGUID))
            {
                throw new ValidationException("Не задан ID", "");
            }
            Activity activity = Database.Activity.GetAll().Where(x => x.IdentityGUID == identityGUID).FirstOrDefault();
            if (activity != null && activity.Mode.Description.Equals("Вход"))
            {
                return true; //Person in the room
            }
            return false;
        }
    }
}