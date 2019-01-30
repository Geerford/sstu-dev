using Domain.Contracts;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    /// <summary>
    /// Implements <see cref="IRepository{T}"/>.
    /// </summary>
    public class IdentityRepository : IRepository<Identity>
    {
        /// <summary>
        /// Store for the <see cref="Context"/> property.
        /// </summary>
        private Context database;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> object.</param>
        public IdentityRepository(Context context)
        {
            database = context;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Create(T)"/>.
        /// </summary>
        public void Create(Identity item)
        {
            database.Identity.Add(item);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Delete(int)"/>.
        /// </summary>
        public void Delete(int id)
        {
            Identity item = database.Identity.Find(id);
            if (item != null)
            {
                database.Identity.Remove(item);
            }
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Find(Func{T, bool})"/>.
        /// </summary>
        public IEnumerable<Identity> Find(Func<Identity, bool> predicate)
        {
            return database.Identity.Where(predicate);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.FindFirst(Func{T, bool})"/>.
        /// </summary>
        public Identity FindFirst(Func<Identity, bool> predicate)
        {
            return database.Identity.Where(predicate).First();
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Get(int)"/>.
        /// </summary>
        public Identity Get(int id)
        {
            return database.Identity.Find(id);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.GetAll"/>.
        /// </summary>
        public IEnumerable<Identity> GetAll()
        {
            return database.Identity;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Update(T)"/>.
        /// </summary>
        public void Update(Identity item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}