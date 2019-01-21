using Domain.Core.Logs;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    [Auditable(AuditScope.ClassAndProperties)]
    public class Type : IDescribable
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [StringLength(20)]
        public string Status { get; set; }
        public ICollection<Checkpoint> Checkpoints { get; set; }

        /// <summary>
        /// Implements <see cref="IDescribable.Describe()"/>
        /// </summary>
        public string Describe()
        {
            dynamic json = new JObject();
            json.ID = ID;
            json.Description = Description;
            json.Status = Status;
            json.Checkpoints = Checkpoints;
            return json.ToString();
        }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is Type item)
            {
                result = ID == item.ID;
                result &= Description.Equals(item.Description);
                result &= Status.Equals(item.Status);
                return result;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= Description.GetHashCode();
            hashcode ^= Status.GetHashCode();
            return hashcode;
        }
    }
}
