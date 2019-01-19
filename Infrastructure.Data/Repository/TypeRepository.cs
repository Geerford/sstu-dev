using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class TypeRepository : IRepository<Domain.Core.Type>
    {
        private Context database;

        public TypeRepository(Context context)
        {
            database = context;
        }

        public void Create(Domain.Core.Type item)
        {
            database.Type.Add(item);
        }

        public void Delete(int id)
        {
            Domain.Core.Type item = database.Type.Find(id);
            if (item != null)
            {
                database.Type.Remove(item);
            }
        }

        public IEnumerable<Domain.Core.Type> Find(Func<Domain.Core.Type, bool> predicate)
        {
            return database.Type.Where(predicate);
        }

        public Domain.Core.Type FindFirst(Func<Domain.Core.Type, bool> predicate)
        {
            return database.Type.Where(predicate).First();
        }

        public Domain.Core.Type Get(int id)
        {
            return database.Type.Find(id);
        }

        public IEnumerable<Domain.Core.Type> GetAll()
        {
            return database.Type;
        }

        public void Update(Domain.Core.Type item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}