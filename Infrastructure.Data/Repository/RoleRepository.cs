using Domain.Contracts;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class RoleRepository : IRepository<Role>
    {
        private Context database;

        public RoleRepository(Context context)
        {
            database = context;
        }

        public void Create(Role item)
        {
            database.Role.Add(item);
        }

        public void Delete(int id)
        {
            Role item = database.Role.Find(id);
            if (item != null)
            {
                database.Role.Remove(item);
            }
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return database.Role.Where(predicate).ToList();
        }

        public Role FindFirst(Func<Role, bool> predicate)
        {
            return database.Role.Where(predicate).First();
        }

        public Role Get(int id)
        {
            return database.Role.Find(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return database.Role;
        }

        public void Update(Role item)
        {
            database.Entry(item).State = EntityState.Modified;
        }
    }
}