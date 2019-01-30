using Domain.Contracts;
using Domain.Core;
using Domain.Core.Logs;
using System;

namespace Repository.Interfaces
{
    /// <summary>
    /// Unit of work interface. Maintains a list of objects affected by a 
    /// business transaction and coordinates the writing out of changes.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="Activity"/> repository.
        /// </summary>
        IRepository<Activity> Activity { get; }

        /// <summary>
        /// Gets the <see cref="Admission"/> repository.
        /// </summary>
        IRepository<Admission> Admission { get; }

        /// <summary>
        /// Gets the <see cref="Audit"/> repository.
        /// </summary>
        IAuditRepository<Audit> Audit { get; }

        /// <summary>
        /// Gets the <see cref="Checkpoint"/> repository.
        /// </summary>
        IRepository<Checkpoint> Checkpoint { get; }

        /// <summary>
        /// Gets the <see cref="CheckpointAdmission"/> repository.
        /// </summary>
        IRepository<CheckpointAdmission> CheckpointAdmission { get; }

        /// <summary>
        /// Gets the <see cref="Identity"/> repository.
        /// </summary>
        IRepository<Identity> Identity { get; }

        /// <summary>
        /// Gets the <see cref="Type"/> repository.
        /// </summary>
        IRepository<Domain.Core.Type> Type { get; }

        /// <summary>
        /// Saves state of <see cref="IUnitOfWork"/> repository.
        /// </summary>
        void Save();
    }
}