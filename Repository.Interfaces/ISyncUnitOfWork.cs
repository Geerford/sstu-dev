using Domain.Contracts;
using Domain.Core;
using System;

namespace Repository.Interfaces
{
    public interface ISyncUnitOfWork : IDisposable
    {
        ISyncRepository<User> User { get; }
    }
}
