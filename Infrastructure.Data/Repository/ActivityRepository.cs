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
    public class ActivityRepository : IRepository<Activity>
    {
        /// <summary>
        /// Store for the <see cref="Context"/> property.
        /// </summary>
        private Context database;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> object.</param>
        public ActivityRepository(Context context)
        {
            database = context;
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Create(T)"/>.
        /// </summary>
        public void Create(Activity item)
        {
            database.Activity.Add(item);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Delete(int)"/>.
        /// </summary>
        public void Delete(int id)
        {
            Activity item = database.Activity.Find(id);
            if (item != null)
            {
                database.Activity.Remove(item);
            }
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Find(Func{T, bool})"/>.
        /// </summary>
        public IEnumerable<Activity> Find(Func<Activity, bool> predicate)
        {
            return database.Activity.Where(predicate);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.FindFirst(Func{T, bool})"/>.
        /// </summary>
        public Activity FindFirst(Func<Activity, bool> predicate)
        {
            return database.Activity.Where(predicate).First();
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Get(int)"/>.
        /// </summary>
        public Activity Get(int id)
        {
            return database.Activity.Where(x => x.ID == id).Include(x => x.Mode).FirstOrDefault();
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.GetAll"/>.
        /// </summary>
        public IEnumerable<Activity> GetAll()
        {
            return database.Activity.Include(x => x.Mode);
        }

        /// <summary>
        /// Implements <see cref="IRepository{T}.Update(T)"/>.
        /// </summary>
        public void Update(Activity item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}