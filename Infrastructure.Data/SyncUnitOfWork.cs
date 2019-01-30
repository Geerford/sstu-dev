using Domain.Contracts;
using Domain.Core;
using Infrastructure.Data.Repository;
using Repository.Interfaces;
using System;

namespace Infrastructure.Data
{
    /// <summary>
    /// Implements <see cref="ISyncUnitOfWork"/>.
    /// </summary>
    public class SyncUnitOfWork : ISyncUnitOfWork
    {
        /// <summary>
        /// Store for the <see cref="Context"/> property.
        /// </summary>
        private SyncContext database;

        /// <summary>
        /// Store true if states have been cleanup; otherwise, false.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Store for the <see cref="UserRepository"/> property.
        /// </summary>
        private UserRepository UserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncUnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SyncUnitOfWork(string connectionString)
        {
            database = new SyncContext(connectionString);
        }

        /// <summary>
        /// Implements <see cref="ISyncUnitOfWork.User"/>.
        /// </summary>
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
    }
}