using Domain.Core;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Service.DTO;

namespace Services.Business.Services
{
    public class CheckpointService : ICheckpointService
    {
        IUnitOfWork Database { get; set; }

        public CheckpointService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(CheckpointDTO model)
        {
            Database.Checkpoint.Create(new Checkpoint
            {
                Campus = model.Campus,
                Row = model.Row,
                Description = model.Description,
                Status = model.Status,
                TypeID = model.Type.ID,
                IP = model.IP
            });
            Database.Save();
            Checkpoint checkpoint = Database.Checkpoint.GetAll().Where(d => d.IP == model.IP).FirstOrDefault();
            if(checkpoint != null)
            {
                int checkpointID = checkpoint.ID;
                foreach (var item in model.Admissions)
                {
                    Database.CheckpointAdmission.Create(new CheckpointAdmission
                    {
                        AdmissionID = item.ID,
                        CheckpointID = checkpointID
                    });
                }
            }
        }

        public void Delete(Checkpoint model)
        {
            Database.Checkpoint.Delete(model.ID);
            Database.Save();
        }

        public void Delete(int? checkpointID, int? itemID)
        {
            Database.CheckpointAdmission.Delete(
                Database.CheckpointAdmission.GetAll()
                    .Where(c => (c.CheckpointID == checkpointID) && (c.AdmissionID == itemID))
                        .FirstOrDefault().ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(CheckpointDTO model)
        {
            Database.Checkpoint.Update(new Checkpoint
            {
                ID = model.ID,
                IP = model.IP,
                Campus = model.Campus,
                Description = model.Description,
                Row = model.Row,
                Status = model.Status,
                TypeID = model.Type.ID
            });
            Database.Save();
            Checkpoint checkpoint = Database.Checkpoint.GetAll().Where(d => d.IP == model.IP).FirstOrDefault();
            if (checkpoint != null)
            {
                int checkpointID = checkpoint.ID;
                foreach (var item in Database.CheckpointAdmission.GetAll().Where(x => x.CheckpointID == model.ID))
                {
                    Database.CheckpointAdmission.Delete(item.ID);
                }
                Database.Save();
                foreach (var item in model.Admissions)
                {
                    Database.CheckpointAdmission.Create(new CheckpointAdmission
                    {
                        AdmissionID = item.ID,
                        CheckpointID = checkpointID
                    });
                }
                Database.Save();
            }
        }

        public Checkpoint GetSimple(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Checkpoint item = Database.Checkpoint.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public CheckpointDTO Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            CheckpointDTO item = GetFull(id);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public CheckpointDTO GetByIP(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                throw new ValidationException("Не задан IP", "");
            }
            Checkpoint item = Database.Checkpoint.Find(x => x.IP == ip).FirstOrDefault();
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return GetFull(item.ID);
        }

        public IEnumerable<CheckpointDTO> GetAll()
        {
            List<CheckpointDTO> result = new List<CheckpointDTO>();
            foreach (var item in Database.Checkpoint.GetAll())
            {
                result.Add(GetFull(item.ID));
            }
            return result;
        }

        public IEnumerable<CheckpointDTO> GetByStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new ValidationException("Не задан статус", "");
            }
            List<CheckpointDTO> result = new List<CheckpointDTO>();
            foreach (var item in Database.Checkpoint.Find(x => x.Status == status))
            {
                result.Add(GetFull(item.ID));
            }
            return result;
        }

        public IEnumerable<CheckpointDTO> GetByType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ValidationException("Не задан тип", "");
            }
            Type typeObject = Database.Type.Find(x => x.Status == type).LastOrDefault();
            if (typeObject == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            List<CheckpointDTO> result = new List<CheckpointDTO>();
            foreach (var item in Database.Checkpoint.Find(x => x.TypeID == typeObject.ID))
            {
                result.Add(GetFull(item.ID));
            }
            return result;
        }

        public CheckpointDTO GetFull(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Checkpoint checkpoint = Database.Checkpoint.Get(id.Value);
            if (checkpoint == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            CheckpointDTO result = (CheckpointDTO)checkpoint;
            Type type = Database.Type.Get(checkpoint.TypeID);
            List<Admission> admissions = new List<Admission>();
            foreach (var item in Database.CheckpointAdmission.GetAll().Where(x => x.CheckpointID == checkpoint.ID))
            {
                Admission admission = Database.Admission.Get(item.AdmissionID);
                if (admission == null)
                {
                    throw new ValidationException("Сущность не найдена", "");
                }
                admissions.Add(admission);
            }
            result.Type = (TypeDTO)type ?? throw new ValidationException("Сущность не найдена", "");
            result.Admissions = admissions;
            return result;
        }
        
        public bool IsMatchAdmission(int checkpointID, int admissionID)
        {
            return (Database.CheckpointAdmission.FindFirst(i => i.CheckpointID == checkpointID && 
                i.AdmissionID == admissionID) != null) ? true : false;
        }
    }
}