using Domain.Contracts;
using Domain.Core;
using Domain.Core.Logs;
using Infrastructure.Data.Repository;
using Newtonsoft.Json.Linq;
using Repository.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context database;
        private bool disposed = false;

        private ActivityRepository ActivityRepository;
        private AdmissionRepository AdmissionRepository; 
        private CheckpointAdmissionRepository CheckpointAdmissionRepository;
        private CheckpointRepository CheckpointRepository;
        private IdentityRepository IdentityRepository;
        private TypeRepository TypeRepository;
        private AuditRepository AuditRepository;

        public UnitOfWork(string connectionString)
        {
            database = new Context(connectionString);
        }

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

        public IAuditRepository Audit
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

        public void Save()
        {
            Logging();
            database.SaveChanges();
        }

        /// <summary>
        /// Save result of operation in Audit SQL-table
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}