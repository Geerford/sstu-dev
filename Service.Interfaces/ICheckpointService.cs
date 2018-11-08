using Domain.Core;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface ICheckpointService
    {
        Checkpoint Get(int? id);
        IEnumerable<Checkpoint> GetAll();
        void Create(Checkpoint model);
        void Edit(Checkpoint model);
        void Delete(Checkpoint model);
        void Dispose();
        IEnumerable<Checkpoint> GetByStatus(string status);
    }
}