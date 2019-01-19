using Domain.Contracts;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class CheckpointRepository : IRepository<Checkpoint>
    {
        private Context database;

        public CheckpointRepository(Context context)
        {
            database = context;
        }

        public void Create(Checkpoint item)
        {
            database.Checkpoint.Add(item);
        }

        public void Delete(int id)
        {
            Checkpoint item = database.Checkpoint.Find(id);
            if (item != null)
            {
                database.Checkpoint.Remove(item);
            }
        }

        public IEnumerable<Checkpoint> Find(Func<Checkpoint, bool> predicate)
        {
            return database.Checkpoint.Where(predicate);
        }

        public Checkpoint FindFirst(Func<Checkpoint, bool> predicate)
        {
            return database.Checkpoint.Where(predicate).First();
        }

        public Checkpoint Get(int id)
        {
            return database.Checkpoint.Find(id);
        }

        public IEnumerable<Checkpoint> GetAll()
        {
            return database.Checkpoint;
        }

        public void Update(Checkpoint item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}