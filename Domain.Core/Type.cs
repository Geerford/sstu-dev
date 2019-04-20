using Domain.Core.Logs;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Core
{
    /// <summary>
    /// Represents a type entity that is given to checkpoint.
    /// </summary>
    [Auditable(AuditScope.ClassAndProperties)]
    public class Type : IDescribable
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        [Index(IsUnique = true)]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type status.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Checkpoint"/>.
        /// </summary>
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

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
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

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            var hashcode = ID.GetHashCode();
            hashcode ^= Description.GetHashCode();
            hashcode ^= Status.GetHashCode();
            return hashcode;
        }
    }
}
