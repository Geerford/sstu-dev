using Domain.Contracts;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repository
{
    public class UserRepository : ISyncRepository<User>
    {
        private SyncContext database;

        public UserRepository(SyncContext context)
        {
            database = context;
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return database.User.Where(predicate);
        }

        public User FindFirst(Func<User, bool> predicate)
        {
            return database.User.Where(predicate).First();
        }

        public User Get(int id)
        {
            return database.User.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return database.User;
        }
    }
}
