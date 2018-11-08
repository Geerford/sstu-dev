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
        Identity Find(Identity model);
        IEnumerable<Identity> GetByStatus(string status);
        Identity GetByRFID(string rfid);
        Identity GetByQR(string qr);
        void Deactivate(int? id);
        void Activate(int? id);
        void Dispose();
    }
}