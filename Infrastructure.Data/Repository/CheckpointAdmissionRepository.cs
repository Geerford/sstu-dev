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
    public class CheckpointAdmissionRepository : IRepository<CheckpointAdmission>
    {
        /// <summary>
        /// Store for the <see cref="Context"/> property.
        /// </summary>
        private Context database;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckpointAdmissionRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> object.</param>
        public CheckpointAdmissionRepository(Context context)
        {
            database = context;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Create(T)"/>.
        /// </summary>
        public void Create(CheckpointAdmission item)
        {
            database.CheckpointAdmission.Add(item);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Delete(int)"/>.
        /// </summary>
        public void Delete(int id)
        {
            CheckpointAdmission item = database.CheckpointAdmission.Find(id);
            if (item != null)
            {
                database.CheckpointAdmission.Remove(item);
            }
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Find(Func{T, bool})"/>.
        /// </summary>
        public IEnumerable<CheckpointAdmission> Find(Func<CheckpointAdmission, bool> predicate)
        {
            return database.CheckpointAdmission.Where(predicate);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.FindFirst(Func{T, bool})"/>.
        /// </summary>
        public CheckpointAdmission FindFirst(Func<CheckpointAdmission, bool> predicate)
        {
            return database.CheckpointAdmission.Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Get(int)"/>.
        /// </summary>
        public CheckpointAdmission Get(int id)
        {
            return database.CheckpointAdmission.Find(id);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.GetAll"/>.
        /// </summary>
        public IEnumerable<CheckpointAdmission> GetAll()
        {
            return database.CheckpointAdmission;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Update(T)"/>.
        /// </summary>
        public void Update(CheckpointAdmission item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}