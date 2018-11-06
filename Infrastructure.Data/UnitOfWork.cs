using Domain.Contracts;
using Domain.Core;
using Infrastructure.Data.Repository;
using Repository.Interfaces;
using System;

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
        private RoleRepository RoleRepository;
        private TypeRepository TypeRepository;

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

        public IRepository<Role> Role
        {
            get
            {
                if (RoleRepository == null)
                {
                    RoleRepository = new RoleRepository(database);
                }
                return RoleRepository;
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

        public void Save()
        {
            database.SaveChanges();
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