using Domain.Core.Logs;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    [Auditable(AuditScope.ClassAndProperties)]
    public class Activity : IDescribable
    {
        public int ID { get; set; }
        [Required]
        public string IdentityGUID { get; set; }
        [Required]
        public string CheckpointIP { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        [StringLength(20)]
        public string Mode { get; set; }

        /// <summary>
        /// Implements <see cref="IDescribable.Describe()"/>
        /// </summary>
        public string Describe()
        {
            dynamic json = new JObject();
            json.ID = ID;
            json.IdentityGUID = IdentityGUID;
            json.CheckpointIP = CheckpointIP;
            json.Date = Date;
            json.Status = Status;
            json.Mode = Mode;
            return json.ToString();
        }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is Activity item)
            {
                result = ID == item.ID;
                result &= IdentityGUID.Equals(item.IdentityGUID);
                result &= CheckpointIP.Equals(item.CheckpointIP);
                result &= Date.Equals(item.Date);
                result &= Status.Equals(item.Status);
                result &= Mode.Equals(item.Mode);
                return result;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= IdentityGUID.GetHashCode();
            hashcode ^= CheckpointIP.GetHashCode();
            hashcode ^= Date.GetHashCode();
            hashcode ^= Status.GetHashCode();
            hashcode ^= Mode.GetHashCode();
            return hashcode;
        }
    }
}
