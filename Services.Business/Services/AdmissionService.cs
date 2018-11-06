using Domain.Core;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services.Business.Services
{
    public class AdmissionService : IAdmissionService
    {
        IUnitOfWork Database { get; set; }

        public AdmissionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(Admission model)
        {
            Database.Admission.Create(model);
            Database.Save();
        }

        public void Delete(Admission model)
        {
            Database.Admission.Delete(model.ID);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Edit(Admission model)
        {
            Database.Admission.Update(model);
            Database.Save();
        }

        public Admission Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Admission item = Database.Admission.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        public IEnumerable<Admission> GetAll()
        {
            return Database.Admission.GetAll().ToList();
        }
    }
}