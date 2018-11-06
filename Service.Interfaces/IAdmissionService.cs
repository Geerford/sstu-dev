using Domain.Core;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IAdmissionService
    {
        Admission Get(int? id);
        IEnumerable<Admission> GetAll();
        void Create(Admission model);
        void Edit(Admission model);
        void Delete(Admission model);
        void Dispose();
    }
}