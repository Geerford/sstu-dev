using Domain.Contracts;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class IdentityRepository : IRepository<Identity>
    {
        private Context database;

        public IdentityRepository(Context context)
        {
            database = context;
        }

        public void Create(Identity item)
        {
            database.Identity.Add(item);
        }

        public void Delete(int id)
        {
            Identity item = database.Identity.Find(id);
            if (item != null)
            {
                database.Identity.Remove(item);
            }
        }

        public IEnumerable<Identity> Find(Func<Identity, bool> predicate)
        {
            return database.Identity.Where(predicate).ToList();
        }

        public Identity FindFirst(Func<Identity, bool> predicate)
        {
            return database.Identity.Where(predicate).First();
        }

        public Identity Get(int id)
        {
            return database.Identity.Find(id);
        }

        public IEnumerable<Identity> GetAll()
        {
            return database.Identity;
        }

        public void Update(Identity item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}