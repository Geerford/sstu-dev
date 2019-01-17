using Domain.Core;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services.Business.Services
{
    public class TypeService : ITypeService
    {
        IUnitOfWork Database { get; set; }

        public TypeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Type model)
        {
            Database.Type.Create(model);
            Database.Save();
        }

        public void Delete(Type model)
        {
            Database.Type.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Type model)
        {
            Database.Type.Update(model);
            Database.Save();
        }

        public Type Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Type item = Database.Type.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Type> GetAll()
        {
            return Database.Type.GetAll().ToList();
        }

        public IEnumerable<Type> GetByStatus(string status)
        {
            return Database.Type.Find(x => x.Status.Equals(status));
        }
    }
}