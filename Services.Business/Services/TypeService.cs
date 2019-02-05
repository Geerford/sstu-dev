using Domain.Core;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;

namespace Services.Business.Services
{
    /// <summary>
    /// Implements <see cref="ITypeService"/>.
    /// </summary>
    public class TypeService : ITypeService
    {
        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeService"/> class.
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> object.</param>
        public TypeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Implements <see cref="ITypeService.Create(Type)"/>.
        /// </summary>
        public void Create(Type model)
        {
            Database.Type.Create(model);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="ITypeService.Delete(Type)"/>.
        /// </summary>
        public void Delete(Type model)
        {
            Database.Type.Delete(model.ID);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="ITypeService.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>
        /// Implements <see cref="ITypeService.Edit(Type)"/>.
        /// </summary>
        public void Edit(Type model)
        {
            Database.Type.Update(model);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="ITypeService.Get(int?)"/>.
        /// </summary>
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

        /// <summary>
        /// Implements <see cref="ITypeService.GetAll"/>.
        /// </summary>
        public IEnumerable<Type> GetAll()
        {
            return Database.Type.GetAll();
        }

        /// <summary>
        /// Implements <see cref="ITypeService.GetByStatus(string)"/>.
        /// </summary>
        public IEnumerable<Type> GetByStatus(string status)
        {
            return Database.Type.Find(x => x.Status.Equals(status));
        }
    }
}