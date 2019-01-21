using Domain.Core.Logs;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    [Auditable(AuditScope.ClassAndProperties)]
    public class Admission : IDescribable
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public string Role { get; set; }

        /// <summary>
        /// Implements <see cref="IDescribable.Describe()"/>
        /// </summary>
        public string Describe()
        {
            dynamic json = new JObject();
            json.ID = ID;
            json.Description = Description;
            json.Role = Role;
            return json.ToString();
        }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is Admission item)
            {
                result = ID == item.ID;
                result &= Description.Equals(item.Description);
                result &= Role.Equals(item.Role);
                return result;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= Description.GetHashCode();
            hashcode ^= Role.GetHashCode();
            return hashcode;
        }
    }
}