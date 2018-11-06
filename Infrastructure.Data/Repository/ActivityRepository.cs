using Domain.Contracts;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class ActivityRepository : IRepository<Activity>
    {
        private Context database;

        public ActivityRepository(Context context)
        {
            database = context;
        }

        public void Create(Activity item)
        {
            database.Activity.Add(item);
        }

        public void Delete(int id)
        {
            Activity item = database.Activity.Find(id);
            if (item != null)
            {
                database.Activity.Remove(item);
            }
        }

        public IEnumerable<Activity> Find(Func<Activity, bool> predicate)
        {
            return database.Activity.Where(predicate).ToList();
        }

        public Activity FindFirst(Func<Activity, bool> predicate)
        {
            return database.Activity.Where(predicate).First();
        }

        public Activity Get(int id)
        {
            return database.Activity.Find(id);
        }

        public IEnumerable<Activity> GetAll()
        {
            return database.Activity;
        }

        public void Update(Activity item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}