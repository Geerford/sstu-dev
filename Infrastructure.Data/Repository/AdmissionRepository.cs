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
    public class AdmissionRepository : IRepository<Admission>
    {
        /// <summary>
        /// Store for the <see cref="Context"/> property.
        /// </summary>
        private Context database;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdmissionRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> object.</param>
        public AdmissionRepository(Context context)
        {
            database = context;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Create(T)"/>.
        /// </summary>
        public void Create(Admission item)
        {
            database.Admission.Add(item);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Delete(int)"/>.
        /// </summary>
        public void Delete(int id)
        {
            Admission item = database.Admission.Find(id);
            if (item != null)
            {
                database.Admission.Remove(item);
            }
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Find(Func{T, bool})"/>.
        /// </summary>
        public IEnumerable<Admission> Find(Func<Admission, bool> predicate)
        {
            return database.Admission.Where(predicate);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.FindFirst(Func{T, bool})"/>.
        /// </summary>
        public Admission FindFirst(Func<Admission, bool> predicate)
        {
            return database.Admission.Where(predicate).First();
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Get(int)"/>.
        /// </summary>
        public Admission Get(int id)
        {
            return database.Admission.Find(id);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.GetAll"/>.
        /// </summary>
        public IEnumerable<Admission> GetAll()
        {
            return database.Admission;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Update(T)"/>.
        /// </summary>
        public void Update(Admission item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}