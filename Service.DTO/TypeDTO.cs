using Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    /// <summary>
    /// Represents a merged type entity. 
    /// </summary>
    public class TypeDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
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
        /// Initializes a <see cref="TypeDTO"/> class.
        /// </summary>
        public TypeDTO() { }

        /// <summary>
        /// Initializes a <see cref="TypeDTO"/> class after cast from <see cref="Type"/> object.
        /// </summary>
        public TypeDTO(Type item)
        {
            ID = item.ID;
            Description = item.Description;
            Status = item.Status;
        }

        /// <summary>
        /// Overrides default type conversion operator to a user-defined type conversion operator 
        /// that must be invoked with a cast.
        /// </summary>
        /// <param name="item">The <see cref="Type"/> object</param>
        public static explicit operator TypeDTO(Type item)
        {
            return new TypeDTO(item);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is TypeDTO item)
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