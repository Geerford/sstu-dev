using Domain.Contracts;
using Domain.Core;
using System;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Activity> Activity { get; }
        IRepository<Admission> Admission { get; }
        IRepository<Checkpoint> Checkpoint { get; }
        IRepository<CheckpointAdmission> CheckpointAdmission { get; }
        IRepository<Identity> Identity { get; }
        IRepository<Role> Role { get; }
        IRepository<Domain.Core.Type> Type { get; }
        void Save();
    }
}