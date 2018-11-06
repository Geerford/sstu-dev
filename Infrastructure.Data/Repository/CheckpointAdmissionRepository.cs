using Domain.Contracts;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class CheckpointAdmissionRepository : IRepository<CheckpointAdmission>
    {
        private Context database;

        public CheckpointAdmissionRepository(Context context)
        {
            database = context;
        }

        public void Create(CheckpointAdmission item)
        {
            database.CheckpointAdmission.Add(item);
        }

        public void Delete(int id)
        {
            CheckpointAdmission item = database.CheckpointAdmission.Find(id);
            if (item != null)
            {
                database.CheckpointAdmission.Remove(item);
            }
        }

        public IEnumerable<CheckpointAdmission> Find(Func<CheckpointAdmission, bool> predicate)
        {
            return database.CheckpointAdmission.Where(predicate).ToList();
        }

        public CheckpointAdmission FindFirst(Func<CheckpointAdmission, bool> predicate)
        {
            return database.CheckpointAdmission.Where(predicate).First();
        }

        public CheckpointAdmission Get(int id)
        {
            return database.CheckpointAdmission.Find(id);
        }

        public IEnumerable<CheckpointAdmission> GetAll()
        {
            return database.CheckpointAdmission;
        }

        public void Update(CheckpointAdmission item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}