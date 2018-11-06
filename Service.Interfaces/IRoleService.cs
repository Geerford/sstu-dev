using Domain.Core;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IRoleService
    {
        Role Get(int? id);
        IEnumerable<Role> GetAll();
        void Create(Role model);
        void Edit(Role model);
        void Delete(Role model);
        void Dispose();
    }
}