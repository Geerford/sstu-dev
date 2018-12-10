using Domain.Contracts;
using Domain.Core;
using Infrastructure.Data.Repository;
using Repository.Interfaces;
using System;

namespace Infrastructure.Data
{
    public class SyncUnitOfWork : ISyncUnitOfWork
    {
        private SyncContext database;
        private bool disposed = false;

        private UserRepository UserRepository;

        public SyncUnitOfWork(string connectionString)
        {
            database = new SyncContext(connectionString);
        }

        public ISyncRepository<User> User
        {
            get
            {
                if (UserRepository == null)
                {
                    UserRepository = new UserRepository(database);
                }
                return UserRepository;
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
