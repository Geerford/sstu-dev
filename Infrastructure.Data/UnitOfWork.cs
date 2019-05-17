using Domain.Contracts;
using Domain.Core;
using Domain.Core.Logs;
using Infrastructure.Data.Repository;
using Newtonsoft.Json.Linq;
using Repository.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Reflection;

namespace Infrastructure.Data
{
    /// <summary>
    /// Implements <see cref="IUnitOfWork"/>.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Store for the <see cref="Context"/> property.
        /// </summary>
        private Context database;

        /// <summary>
        /// Store true if states have been cleanup; otherwise, false.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Store for the <see cref="ActivityRepository"/> property.
        /// </summary>
        private ActivityRepository ActivityRepository;

        /// <summary>
        /// Store for the <see cref="AdmissionRepository"/> property.
        /// </summary>
        private AdmissionRepository AdmissionRepository;

        /// <summary>
        /// Store for the <see cref="AuditRepository"/> property.
        /// </summary>
        private AuditRepository AuditRepository;

        /// <summary>
        /// Store for the <see cref="CheckpointAdmissionRepository"/> property.
        /// </summary>
        private CheckpointAdmissionRepository CheckpointAdmissionRepository;

        /// <summary>
        /// Store for the <see cref="CheckpointRepository"/> property.
        /// </summary>
        private CheckpointRepository CheckpointRepository;

        /// <summary>
        /// Store for the <see cref="IdentityRepository"/> property.
        /// </summary>
        private IdentityRepository IdentityRepository;

        /// <summary>
        /// Store for the <see cref="ModeRepository"/> property.
        /// </summary>
        private ModeRepository ModeRepository;

        /// <summary>
        /// Store for the <see cref="TypeRepository"/> property.
        /// </summary>
        private TypeRepository TypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public UnitOfWork(string connectionString)
        {
            database = new Context(connectionString);
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Activity"/>.
        /// </summary>
        public IRepository<Activity> Activity
        {
            get
            {
                if (ActivityRepository == null)
                {
                    ActivityRepository = new ActivityRepository(database);
                }
                return ActivityRepository;
            }
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Admission"/>.
        /// </summary>
        public IRepository<Admission> Admission
        {
            get
            {
                if (AdmissionRepository == null)
                {
                    AdmissionRepository = new AdmissionRepository(database);
                }
                return AdmissionRepository;
            }
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Audit"/>.
        /// </summary>
        public IAuditRepository<Audit> Audit
        {
            get
            {
                if (AuditRepository == null)
                {
                    AuditRepository = new AuditRepository(database);
                }
                return AuditRepository;
            }
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.CheckpointAdmission"/>.
        /// </summary>
        public IRepository<CheckpointAdmission> CheckpointAdmission
        {
            get
            {
                if (CheckpointAdmissionRepository == null)
                {
                    CheckpointAdmissionRepository = new CheckpointAdmissionRepository(database);
                }
                return CheckpointAdmissionRepository;
            }
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Checkpoint"/>.
        /// </summary>
        public IRepository<Checkpoint> Checkpoint
        {
            get
            {
                if (CheckpointRepository == null)
                {
                    CheckpointRepository = new CheckpointRepository(database);
                }
                return CheckpointRepository;
            }
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Identity"/>.
        /// </summary>
        public IRepository<Identity> Identity
        {
            get
            {
                if (IdentityRepository == null)
                {
                    IdentityRepository = new IdentityRepository(database);
                }
                return IdentityRepository;
            }
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Mode"/>.
        /// </summary>
        public IRepository<Mode> Mode
        {
            get
            {
                if (ModeRepository == null)
                {
                    ModeRepository = new ModeRepository(database);
                }
                return ModeRepository;
            }
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Type"/>.
        /// </summary>
        public IRepository<Domain.Core.Type> Type
        {
            get
            {
                if (TypeRepository == null)
                {
                    TypeRepository = new TypeRepository(database);
                }
                return TypeRepository;
            }
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Save"/>.
        /// </summary>
        public void Save()
        {
            Logging();
            database.SaveChanges();
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.SaveSync"/>.
        /// </summary>
        public void SaveSync()
        {
            database.SaveChanges();
        }

        /// <summary>
        /// Saves state change in Audit SQL-table
        /// </summary>
        private void Logging()
        {
            foreach(DbEntityEntry item in database.ChangeTracker.Entries())
            {
                System.Type itemType = item.State == EntityState.Deleted ? item.OriginalValues.ToObject().GetType() : item.CurrentValues.ToObject().GetType();
                AuditableAttribute auditableAttribute = itemType.GetCustomAttribute<AuditableAttribute>();

                switch (item.State)
                {
                    case EntityState.Added:
                    case EntityState.Deleted:
                        switch (auditableAttribute.AuditScope)
                        {
                            case AuditScope.ClassOnly:
                            case AuditScope.ClassAndProperties:
                                var describable = item.Entity as IDescribable;
                                if (describable != null)
                                {
                                    dynamic auditLog = new JObject();
                                    auditLog.State = item.State.ToString();
                                    auditLog.Data = describable.Describe();
                                    Audit audit = new Audit
                                    {
                                        EntityName = item.Entity.GetType().Name,
                                        Logs = auditLog.ToString(),
                                        ModifiedDate = DateTime.Now,
                                        ModifiedBy = Environment.UserDomainName != null ? $"{Environment.UserName}@{Environment.UserDomainName}" : $"{Environment.UserName}",
                                        ModifiedFrom = Environment.MachineName
                                    };
                                    Audit.Create(audit);
                                }
                                break;
                            default:
                                continue;
                        }
                        break;
                    case EntityState.Modified:
                        switch (auditableAttribute.AuditScope)
                        {
                            case AuditScope.PropertiesOnly:
                            case AuditScope.ClassAndProperties:
                                foreach (string propertyName in item.OriginalValues.PropertyNames)
                                {
                                    var originalValue = item.GetDatabaseValues().GetValue<object>(propertyName);
                                    var currentValue = item.CurrentValues.GetValue<object>(propertyName);
                                    if (!Equals(originalValue, currentValue))
                                    {
                                        dynamic auditLog = new JObject();
                                        auditLog.State = item.State.ToString();
                                        auditLog.PropertyName = propertyName;
                                        auditLog.OriginalValue = originalValue;
                                        auditLog.NewValue = currentValue;
                                        Audit audit = new Audit
                                        {
                                            EntityName = item.Entity.GetType().Name,
                                            Logs = auditLog.ToString(),
                                            ModifiedDate = DateTime.Now,
                                            ModifiedBy = Environment.UserDomainName != null ? $"{Environment.UserName}@{Environment.UserDomainName}" : $"{Environment.UserName}",
                                            ModifiedFrom = Environment.MachineName
                                        };
                                        Audit.Create(audit);
                                    }
                                }
                                break;
                            default:
                                continue;
                        }
                        break;
                    //In case that item.State equals EntityState.DETACHED and EntityState.UNCHANGED
                    default:
                        continue;
                }
            }
        }

        /// <summary>
        /// Overloaded Implementation of Dispose. Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    database.Dispose();
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Performs all object cleanup. Frees unmanaged resources and indicates that the 
        /// finalizer, if one is present, doesn't have to run.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Backup"/>.
        /// </summary>
        public string Backup()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = database.Database.Connection.ConnectionString
            };
            connection.Open();
            string dbName = connection.Database;
            connection.Close();
            string dbBackupPath = AppDomain.CurrentDomain.GetData("DataDirectory") + "\\DB_" +
                DateTime.Now.ToString("yyyyMMddHHmm") + ".bak";
            string command = $"BACKUP DATABASE [{dbName}] TO DISK = '{dbBackupPath}' " + @"
                                WITH NOFORMAT, NOINIT, NAME = N'Context-Full Database Backup',
                                    SKIP, NOREWIND, NOUNLOAD, STATS = 10";
            database.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, command);
            return dbBackupPath;
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Drop"/>.
        /// </summary>
        public void Drop()
        {
            database.Database.Delete();
            database.SaveChanges();
            database = new Context("Context");
        }

        /// <summary>
        /// Implements <see cref="IUnitOfWork.Restore(string)"/>.
        /// </summary>
        public void Restore(string backupName)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = database.Database.Connection.ConnectionString
            };
            connection.Open();
            string dbName = connection.Database;
            connection.Close();
            string path = AppDomain.CurrentDomain.GetData("DataDirectory") + "\\" + backupName;
            string command = "use [master];" + 
                        $"ALTER DATABASE {dbName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;" + 
                        $"RESTORE DATABASE {dbName} FROM DISK = N'{path}' WITH NOUNLOAD, REPLACE, STATS = 10;" +
                        $"ALTER DATABASE {dbName} SET MULTI_USER;";
            database.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, command);
        }
    }
}