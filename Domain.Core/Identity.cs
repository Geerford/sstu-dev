using Domain.Core.Logs;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Core
{
    /// <summary>
    /// Represents a user entity.
    /// </summary>
    [Auditable(AuditScope.ClassAndProperties)]
    public class Identity : IDescribable
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string GUID { get; set; }

        /// <summary>
        /// Gets or sets the user picture.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Implements <see cref="IDescribable.Describe()"/>
        /// </summary>
        public string Describe()
        {
            dynamic json = new JObject();
            json.ID = ID;
            json.GUID = GUID;
            json.Picture = Picture;
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
            if (obj is Identity item)
            {
                result = ID == item.ID;
                result &= GUID.Equals(item.GUID);
                result &= Picture.Equals(item.Picture);
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
            hashcode ^= GUID.GetHashCode();
            hashcode ^= Picture.GetHashCode();
            return hashcode;
        }
    }
}