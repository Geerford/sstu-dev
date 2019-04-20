using Repository.Interfaces;
using Service.Interfaces;
using System;

namespace Services.Business.Service
{
    /// <summary>
    /// Implements <see cref="IDatabaseService"/>.
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        /// <summary>
        /// Gets or sets the repository context.
        /// </summary>
        IUnitOfWork Database { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityService"/> class.
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> object.</param>
        /// /// <param name="suow">The <see cref="ISyncUnitOfWork"/> object.</param>
        public DatabaseService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Implements <see cref="IDatabaseService.Backup"/>.
        /// </summary>
        public string Backup()
        {
            string dbBackupName;
            try
            {
                dbBackupName = Database.Backup();
            }
            catch (Exception)
            {
                throw new ValidationException("Не удалось создать резервную копию", "");
            }
            try
            {
                Database.Drop();
            }
            catch (Exception)
            {
                throw new ValidationException("Не удалось пересоздать базу данных", "");
            }
            return dbBackupName;
        }

        /// <summary>
        /// Implements <see cref="IDatabaseService.Recovery(string)"/>.
        /// </summary>
        public void Recovery(string backupName)
        {
            try
            {
                Database.Restore(backupName);
            }
            catch (Exception)
            {
                throw new ValidationException("Не удалось восстановить базу данных", "");
            }
        }
    }
}