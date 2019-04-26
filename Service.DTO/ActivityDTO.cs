using Domain.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace Service.DTO
{
    /// <summary>
    /// Represents a merged checkpoint entity. 
    /// </summary>
    public class ActivityDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [Required]
        public IdentityDTO User { get; set; }

        /// <summary>
        /// Gets or sets the checkpoint.
        /// </summary>
        [Required]
        public CheckpointDTO Checkpoint { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the event status. True equals success and false equals failure.
        /// </summary>
        [Required]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the event mode.
        /// </summary>
        [Required]
        public Mode Mode { get; set; }
        
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is ActivityDTO item)
            {
                result = ID == item.ID;
                result &= User.Equals(item.User);
                result &= Checkpoint.Equals(item.Checkpoint);
                result &= Date.Equals(item.Date);
                result &= Status.Equals(item.Status);
                result &= Mode.Equals(item.Mode);
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
            hashcode ^= User.GetHashCode();
            hashcode ^= Checkpoint.GetHashCode();
            hashcode ^= Date.GetHashCode();
            hashcode ^= Status.GetHashCode();
            hashcode ^= Mode.GetHashCode();
            return hashcode;
        }
    }
}