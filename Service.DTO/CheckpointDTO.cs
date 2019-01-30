using Domain.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    /// <summary>
    /// Represents a merged checkpoint entity. 
    /// </summary>
    public class CheckpointDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
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
        public int Campus { get; set; }

        /// <summary>
        /// Gets or sets the floor.
        /// </summary>
        [Required]
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the checkpoint status.
        /// </summary>
        [Required]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the checkpoint type.
        /// </summary>
        [Required]
        public TypeDTO Type { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Admission"/>.
        /// </summary>
        [Required]
        public IEnumerable<Admission> Admissions { get; set; }

        /// <summary>
        /// Initializes a <see cref="CheckpointDTO"/> class.
        /// </summary>
        public CheckpointDTO()
        {
            Admissions = new List<Admission>();
        }

        /// <summary>
        /// Initializes a <see cref="CheckpointDTO"/> class after cast from <see cref="Checkpoint"/> object.
        /// </summary>
        public CheckpointDTO(Checkpoint item)
        {
            ID = item.ID;
            IP = item.IP;
            Campus = item.Campus;
            Row = item.Row;
            Description = item.Description;
            Status = item.Status;
        }

        /// <summary>
        /// Overrides default type conversion operator to a user-defined type conversion operator 
        /// that must be invoked with a cast.
        /// </summary>
        /// <param name="item">The <see cref="Checkpoint"/> object</param>
        public static explicit operator CheckpointDTO(Checkpoint item)
        {
            return new CheckpointDTO(item);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is CheckpointDTO item)
            {
                result = ID == item.ID;
                result &= IP.Equals(item.IP);
                result &= Campus.Equals(item.Campus);
                result &= Row.Equals(item.Row);
                result &= Description.Equals(item.Description);
                result &= Status.Equals(item.Status);
                result &= Type.Equals(item.Type);
                result &= Admissions.Equals(item.Admissions);
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
            hashcode ^= Admissions.GetHashCode();
            return hashcode;
        }
    }
}