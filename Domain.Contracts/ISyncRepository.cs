using System;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface ISyncRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        T FindFirst(Func<T, bool> predicate);
    }
}