using Domain.Core;
using Service.DTO;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface ICheckpointService
    {
        Checkpoint GetSimple(int? id);
        CheckpointDTO Get(int? id);
        CheckpointDTO GetByIP(string ip);
        CheckpointDTO GetFull(int? id);
        IEnumerable<CheckpointDTO> GetAll();
        IEnumerable<CheckpointDTO> GetByStatus(string status);
        IEnumerable<CheckpointDTO> GetByType(string type);
        void Create(CheckpointDTO model);
        void Edit(CheckpointDTO model);
        void Delete(Checkpoint model);
        void Delete(int? checkpointID, int? itemID);
        void DeleteAllAdmission(int? checkpointID);
        void Dispose();
        bool IsMatchAdmission(int checkpoingID, int admissionID);
    }
}