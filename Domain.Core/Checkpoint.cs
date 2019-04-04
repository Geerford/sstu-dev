using Domain.Core.Logs;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    /// <summary>
    /// Represents a checkpoint entity.
    /// </summary>
    [Auditable(AuditScope.ClassAndProperties)]
    public class Checkpoint : IDescribable
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the IP-address.
        /// </summary>
        [Required]
        public string IP { get; set; }

        /// <summary>
        /// Gets or sets the campus.
        /// </summary>
        [Required]
        public int? Campus { get; set; }

        /// <summary>
        /// Gets or sets the floor.
        /// </summary>
        [Required]
        public int? Row { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        [Required]
        public int? Section { get; set; }

        /// <summary>
        /// Gets or sets the classroom.
        /// </summary>
        public int? Classroom { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the checkpoint status.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the ID of checkpoint type.
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// Gets or sets the checkpoint type.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Implements <see cref="IDescribable.Describe()"/>
        /// </summary>
        public string Describe()
        {
            dynamic json = new JObject();
            json.ID = ID;
            json.IP = IP;
            json.Campus = Campus;
            json.Row = Row;
            json.Description = Description;
            json.Status = Status;
            json.Type = Type;
            json.TypeID = TypeID;
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
            if (obj is Checkpoint item)
            {
                result = ID == item.ID;
                result &= IP.Equals(item.IP);
                result &= Campus.Equals(item.Campus);
                result &= Row.Equals(item.Row);
                result &= Description.Equals(item.Description);
                result &= Status.Equals(item.Status);
                result &= Type.Equals(item.Type);
                result &= TypeID.Equals(item.TypeID);
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
            hashcode ^= IP.GetHashCode();
            hashcode ^= Campus.GetHashCode();
            hashcode ^= Row.GetHashCode();
            hashcode ^= Description.GetHashCode();
            hashcode ^= Status.GetHashCode();
            hashcode ^= Type.GetHashCode();
            hashcode ^= TypeID.GetHashCode();
            return hashcode;
        }
    }
}