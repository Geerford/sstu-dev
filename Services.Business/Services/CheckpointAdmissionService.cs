using System;
using Domain.Core;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services.Business.Services
{
    public class CheckpointAdmissionService : ICheckpointAdmissionService
    {
        IUnitOfWork Database { get; set; }

        public CheckpointAdmissionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(CheckpointAdmission model)
        {
            Database.CheckpointAdmission.Create(model);
            Database.Save();
        }

        public void Delete(CheckpointAdmission model)
        {
            Database.CheckpointAdmission.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(CheckpointAdmission model)
        {
            Database.CheckpointAdmission.Update(model);
            Database.Save();
        }

        public CheckpointAdmission Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            CheckpointAdmission item = Database.CheckpointAdmission.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<CheckpointAdmission> GetAll()
        {
            return Database.CheckpointAdmission.GetAll().ToList();
        }
    }
}