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
    public class ModeRepository : IRepository<Mode>
    {
        /// <summary>
        /// Store for the <see cref="Context"/> property.
        /// </summary>
        private Context database;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModeRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> object.</param>
        public ModeRepository(Context context)
        {
            database = context;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Create(T)"/>.
        /// </summary>
        public void Create(Mode item)
        {
            database.Mode.Add(item);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Delete(int)"/>.
        /// </summary>
        public void Delete(int id)
        {
            Mode item = database.Mode.Find(id);
            if (item != null)
            {
                database.Mode.Remove(item);
            }
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Find(Func{T, bool})"/>.
        /// </summary>
        public IEnumerable<Mode> Find(Func<Mode, bool> predicate)
        {
            return database.Mode.Where(predicate);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.FindFirst(Func{T, bool})"/>.
        /// </summary>
        public Mode FindFirst(Func<Mode, bool> predicate)
        {
            return database.Mode.Where(predicate).First();
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Get(int)"/>.
        /// </summary>
        public Mode Get(int id)
        {
            return database.Mode.Find(id);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.GetAll"/>.
        /// </summary>
        public IEnumerable<Mode> GetAll()
        {
            return database.Mode;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Update(T)"/>.
        /// </summary>
        public void Update(Mode item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}
