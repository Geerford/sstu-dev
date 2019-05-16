using Domain.Core;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;

namespace Services.Business.Service
{
    /// <summary>
    /// Implements <see cref="ITypeService"/>.
    /// </summary>
    public class ModeService : IModeService
    {
        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModeService"/> class.
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> object.</param>
        public ModeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Implements <see cref="IModeService.Create(Mode)"/>.
        /// </summary>
        public void Create(Mode model)
        {
            Database.Mode.Create(model);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IModeService.Delete(Mode)"/>.
        /// </summary>
        public void Delete(Mode model)
        {
            Database.Mode.Delete(model.ID);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IModeService.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>
        /// Implements <see cref="IModeService.Edit(Mode)"/>.
        /// </summary>
        public void Edit(Mode model)
        {
            Database.Mode.Update(model);
            Database.Save();
        }

        /// <summary>
        /// Implements <see cref="IModeService.Get(int?)"/>.
        /// </summary>
        public Mode Get(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не задан ID", "");
            }
            Mode item = Database.Mode.Get(id.Value);
            if (item == null)
            {
                throw new ValidationException("Сущность не найдена", "");
            }
            return item;
        }

        /// <summary>
        /// Implements <see cref="IModeService.GetAll"/>.
        /// </summary>
        public IEnumerable<Mode> GetAll()
        {
            return Database.Mode.GetAll();
        }

        /// <summary>
        /// Implements <see cref="IModeService.GetByMode(string)"/>.
        /// </summary>
        public IEnumerable<Mode> GetByMode(string mode)
        {
            return Database.Mode.Find(x => x.Status.Equals(mode));
        }
    }
}