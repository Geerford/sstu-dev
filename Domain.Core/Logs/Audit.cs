using System;

namespace Domain.Core.Logs
{
    public class Audit
    {
        public int ID { get; set; }
        public string EntityName { get; set; }
        public string Logs { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedFrom { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}