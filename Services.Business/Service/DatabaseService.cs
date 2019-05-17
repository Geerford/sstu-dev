using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Linq;

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
        /// Gets or sets the repository context.
        /// </summary>
        ISyncUnitOfWork SyncDatabase { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityService"/> class.
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> object.</param>
        /// /// <param name="suow">The <see cref="ISyncUnitOfWork"/> object.</param>
        public DatabaseService(IUnitOfWork uow, ISyncUnitOfWork suow)
        {
            Database = uow;
            SyncDatabase = suow;
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
        /// <param name="backupName">The database backup name.</param>
        /// <returns>The operation status.</returns>
        public bool Recovery(string backupName)
        {
            try
            {
                Database.Restore(backupName);
                return true;
            }
            catch (Exception)
            {
                throw new ValidationException("Не удалось восстановить базу данных", "");
            }
        }

        /// <summary>
        /// Implements <see cref="IDatabaseService.Drop()"/>.
        /// </summary>
        /// <returns>The operation status.</returns>
        public bool Drop()
        {
            try
            {
                foreach(var item in Database.Identity.GetAll())
                {
                    if (!item.GUID.Contains("GUEST"))
                    {
                        Database.Identity.Delete(item.ID);
                    }
                }
                Database.SaveSync();
                SyncDatabase.Drop();
                foreach (var item in SyncDatabase.User.GetAll())
                {
                    Database.Identity.Create(new Domain.Core.Identity
                    {
                        GUID = item.GUID,
                        Picture = "cat.jpg"
                    });
                    Database.SaveSync();
                }
                return true;
            }
            catch (Exception)
            {
                throw new ValidationException("Не удалось пересоздать базу данных", "");
            }
        }
    }
}