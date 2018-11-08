using System;
using Domain.Core;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services.Business.Services
{
    public class CheckpointService : ICheckpointService
    {
        IUnitOfWork Database { get; set; }

        public CheckpointService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Checkpoint model)
        {
            Database.Checkpoint.Create(model);
            Database.Save();
        }

        public void Delete(Checkpoint model)
        {
            Database.Checkpoint.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Checkpoint model)
        {
            Database.Checkpoint.Update(model);
            Database.Save();
        }

        public Checkpoint Get(int? id)
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

        public IEnumerable<Checkpoint> GetAll()
        {
            return Database.Checkpoint.GetAll().ToList();
        }

        public IEnumerable<Checkpoint> GetByStatus(string status)
        {
            return Database.Checkpoint.Find(x => x.Status == status);
        }
    }
}