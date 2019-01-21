using Domain.Core.Logs;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    [Auditable(AuditScope.ClassAndProperties)]
    public class CheckpointAdmission : IDescribable
    {
        public int ID { get; set; }
        [Required]
        public int CheckpointID { get; set; }
        [Required]
        public int AdmissionID { get; set; }

        /// <summary>
        /// Implements <see cref="IDescribable.Describe()"/>
        /// </summary>
        public string Describe()
        {
            dynamic json = new JObject();
            json.ID = ID;
            json.CheckpointID = CheckpointID;
            json.AdmissionID = AdmissionID;
            return json.ToString();
        }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is CheckpointAdmission item)
            {
                result = ID == item.ID;
                result &= CheckpointID.Equals(item.CheckpointID);
                result &= AdmissionID.Equals(item.AdmissionID);
                return result;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= CheckpointID.GetHashCode();
            hashcode ^= AdmissionID.GetHashCode();
            return hashcode;
        }
    }
}