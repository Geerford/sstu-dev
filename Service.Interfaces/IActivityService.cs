using Domain.Core;
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
        void Dispose();
    }
}