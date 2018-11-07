using Domain.Core;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface ICheckpointAdmissionService
    {
        CheckpointAdmission Get(int? id);
        IEnumerable<CheckpointAdmission> GetAll();
        void Create(CheckpointAdmission model);
        void Edit(CheckpointAdmission model);
        void Delete(CheckpointAdmission model);
        void Dispose();
        bool IsMatch(int checkpoingID, int admissionID);
    }
}