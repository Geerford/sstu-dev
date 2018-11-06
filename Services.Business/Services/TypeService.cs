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

        public void Create(Domain.Core.Type model)
        {
            Database.Type.Create(model);
            Database.Save();
        }

        public void Delete(Domain.Core.Type model)
        {
            Database.Type.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Domain.Core.Type model)
        {
            Database.Type.Update(model);
            Database.Save();
        }

        public Domain.Core.Type Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Domain.Core.Type item = Database.Type.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Domain.Core.Type> GetAll()
        {
            return Database.Type.GetAll().ToList();
        }
    }
}