using Domain.Core.Logs;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IAuditRepository
    {
        IEnumerable<Audit> GetAll();
        void Create(Audit item);
    }
}