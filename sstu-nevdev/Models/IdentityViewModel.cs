using Domain.Core;
using Service.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents a identity model for MVC.
    /// </summary>
    public class IdentityViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        [Required(ErrorMessage = "GUID должен быть заполнен")]
        public string GUID { get; set; }

        /// <summary>
        /// Gets or sets the picture.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Initializes a <see cref="IdentityDTO"/> class.
        /// </summary>
        public IdentityViewModel() { }

        /// <summary>
        /// Initializes a <see cref="IdentityDTO"/> class after cast from <see cref="Identity"/> object.
        /// </summary>
        public IdentityViewModel(Identity item)
        {
            ID = item.ID;
            GUID = item.GUID;
            Picture = item.Picture;
        }

        /// <summary>
        /// Overrides default type conversion operator to a user-defined type conversion operator 
        /// that must be invoked with a cast.
        /// </summary>
        /// <param name="item">The <see cref="Identity"/> object</param>
        public static explicit operator IdentityViewModel(Identity item)
        {
            return new IdentityViewModel(item);
        }
    }

    /// <summary>
    /// Represents a identity details model for MVC.
    /// </summary>
    public class IdentityDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public IdentityDTO User { get; set; }

        /// <summary>
        /// Gets or sets the activities.
        /// </summary>
        public IEnumerable<Activity> Activities { get; set; }
    }
}