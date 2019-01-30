using Domain.Contracts;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    /// <summary>
    /// Implements <see cref="ISyncRepository{T}"/>.
    /// </summary>
    public class UserRepository : ISyncRepository<User>
    {
        /// <summary>
        /// Store for the <see cref="Context"/> property.
        /// </summary>
        private SyncContext database;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="SyncContext"/> object.</param>
        public UserRepository(SyncContext context)
        {
            database = context;
        }

        /// <summary>
        /// Implements <see cref="ISyncRepository.Find(Func{T, bool})"/>
        /// </summary>
        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return database.User.Where(predicate);
        }

        /// <summary>
        /// Implements <see cref="ISyncRepository.FindFirst(Func{T, bool})"/>
        /// </summary>
        public User FindFirst(Func<User, bool> predicate)
        {
            return database.User.Where(predicate).First();
        }

        /// <summary>
        /// Implements <see cref="ISyncRepository.Get(int)"/>
        /// </summary>
        public User Get(int id)
        {
            return database.User.Find(id);
        }

        /// <summary>
        /// Implements <see cref="ISyncRepository.GetAll"/>
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            return database.User;
        }
    }
}