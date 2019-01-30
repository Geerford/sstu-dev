using Domain.Core.Logs;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    /// <summary>
    /// Represents a checkpoint and permission that is given to this checkpoint. 
    /// </summary>
    [Auditable(AuditScope.ClassAndProperties)]
    public class CheckpointAdmission : IDescribable
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the checkpoint ID.
        /// </summary>
        [Required]
        public int CheckpointID { get; set; }

        /// <summary>
        /// Gets or sets the admission ID.
        /// </summary>
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

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
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

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= CheckpointID.GetHashCode();
            hashcode ^= AdmissionID.GetHashCode();
            return hashcode;
        }
    }
}