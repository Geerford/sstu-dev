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
    public class CheckpointRepository : IRepository<Checkpoint>
    {
        /// <summary>
        /// Store for the <see cref="Context"/> property.
        /// </summary>
        private Context database;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckpointRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> object.</param>
        public CheckpointRepository(Context context)
        {
            database = context;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Create(T)"/>.
        /// </summary>
        public void Create(Checkpoint item)
        {
            database.Checkpoint.Add(item);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Delete(int)"/>.
        /// </summary>
        public void Delete(int id)
        {
            Checkpoint item = database.Checkpoint.Find(id);
            if (item != null)
            {
                database.Checkpoint.Remove(item);
            }
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Find(Func{T, bool})"/>.
        /// </summary>
        public IEnumerable<Checkpoint> Find(Func<Checkpoint, bool> predicate)
        {
            return database.Checkpoint.Where(predicate);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.FindFirst(Func{T, bool})"/>.
        /// </summary>
        public Checkpoint FindFirst(Func<Checkpoint, bool> predicate)
        {
            return database.Checkpoint.Where(predicate).First();
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Get(int)"/>.
        /// </summary>
        public Checkpoint Get(int id)
        {
            return database.Checkpoint.Find(id);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.GetAll"/>.
        /// </summary>
        public IEnumerable<Checkpoint> GetAll()
        {
            return database.Checkpoint;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Update(T)"/>.
        /// </summary>
        public void Update(Checkpoint item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}