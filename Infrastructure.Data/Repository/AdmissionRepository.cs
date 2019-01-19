using Domain.Contracts;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class AdmissionRepository : IRepository<Admission>
    {
        private Context database;

        public AdmissionRepository(Context context)
        {
            database = context;
        }

        public void Create(Admission item)
        {
            database.Admission.Add(item);
        }

        public void Delete(int id)
        {
            Admission item = database.Admission.Find(id);
            if (item != null)
            {
                database.Admission.Remove(item);
            }
        }

        public IEnumerable<Admission> Find(Func<Admission, bool> predicate)
        {
            return database.Admission.Where(predicate);
        }

        public Admission FindFirst(Func<Admission, bool> predicate)
        {
            return database.Admission.Where(predicate).First();
        }

        public Admission Get(int id)
        {
            return database.Admission.Find(id);
        }

        public IEnumerable<Admission> GetAll()
        {
            return database.Admission;
        }

        public void Update(Admission item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}