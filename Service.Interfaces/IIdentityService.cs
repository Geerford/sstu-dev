using Domain.Core;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IIdentityService
    {
        Identity Get(int? id);
        IEnumerable<Identity> GetAll();
        void Create(Identity model);
        void Edit(Identity model);
        void Delete(Identity model);
        void Dispose();
    }
}