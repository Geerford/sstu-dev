using Domain.Contracts;
using Domain.Core.Logs;
using System.Collections.Generic;

namespace Infrastructure.Data.Repository
{
    public class AuditRepository : IAuditRepository
    {
        private Context database;

        public AuditRepository(Context context)
        {
            database = context;
        }

        public IEnumerable<Audit> GetAll()
        {
            return database.Audits;
        }

        public void Create(Audit item)
        {
            database.Audits.Add(item);
        }
    }
}