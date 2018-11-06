using System;
using Domain.Core;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services.Business.Services
{
    public class IdentityService : IIdentityService
    {
        IUnitOfWork Database { get; set; }

        public IdentityService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Identity model)
        {
            Database.Identity.Create(model);
            Database.Save();
        }

        public void Delete(Identity model)
        {
            Database.Identity.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Identity model)
        {
            Database.Identity.Update(model);
            Database.Save();
        }

        public Identity Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Identity item = Database.Identity.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Identity> GetAll()
        {
            return Database.Identity.GetAll().ToList();
        }
    }
}