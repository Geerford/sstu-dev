using Domain.Core;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    /// <summary>
    /// Implements <see cref="IAdmissionService"/>.
    /// </summary>
    public class AdmissionService : IAdmissionService
    {
        /// /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdmissionService"/> class.
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> object.</param>
        public AdmissionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Implements <see cref="IAdmissionService.Create(Admission)"/>
        /// </summary>
        public void Create(Admission model)
        {
            Database.Admission.Create(model);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IAdmissionService.Delete(Admission)"/>
        /// </summary>
        public void Delete(Admission model)
        {
            Database.Admission.Delete(model.ID);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IAdmissionService.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>
        /// Implements <see cref="IAdmissionService.Edit(Admission)"/>
        /// </summary>
        public void Edit(Admission model)
        {
            Database.Admission.Update(model);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IAdmissionService.Get(int?)"/>
        /// </summary>
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

        /// <summary>
        /// Implements <see cref="IAdmissionService.GetAll"/>
        /// </summary>
        public IEnumerable<Admission> GetAll()
        {
            return Database.Admission.GetAll();
        }
    }
}