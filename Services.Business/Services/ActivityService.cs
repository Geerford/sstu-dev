using Domain.Core;
using Repository.Interfaces;
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

        public bool IsAdmission(int? checkpointID, int? roleID)
        {
            if (checkpointID == null || roleID == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            foreach (CheckpointAdmission item in Database.CheckpointAdmission.GetAll().Where(x => x.CheckpointID == checkpointID))
            {
                Admission admission = Database.Admission.Get(item.AdmissionID);
                Role role = Database.Role.Get((int)roleID);
                if (admission.Description.Contains(role.Description))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsPassed(int? identityID)
        {
            if (identityID == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Activity activity = Database.Activity.GetAll().Where(x => x.IdentityID == identityID).LastOrDefault();
            if (activity != null && activity.Mode == "Вход")
            {
                return true; //Person in the room
            }
            return false;
        }
    }
}