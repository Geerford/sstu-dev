using Domain.Core;
using Service.DTO;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IActivityService
    {
        Activity Get(int? id);
        IEnumerable<Activity> GetAll();
        void Create(Activity model);
        void Edit(Activity model);
        void Delete(Activity model);
        IEnumerable<Activity> GetByStatus(bool status);
        bool IsAdmission(int? checkpointID, string role);
        bool IsPassed(string IdentityGUID);
        int IsOk(CheckpointDTO checkpoint, IdentityDTO identity);
        void Dispose();
    }
}