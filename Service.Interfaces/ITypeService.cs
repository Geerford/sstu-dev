using Domain.Core;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface ITypeService
    {
        Type Get(int? id);
        IEnumerable<Type> GetAll();
        void Create(Type model);
        void Edit(Type model);
        void Delete(Type model);
        void Dispose();
        IEnumerable<Type> GetByStatus(string status);
    }
}