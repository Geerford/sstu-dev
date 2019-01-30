using Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents a admission model for MVC.
    /// </summary>
    public class AdmissionViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required(ErrorMessage = "Описание должно быть заполнено")]
        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        [Required(ErrorMessage = "Выберите роль")]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SelectList"/> of roles.
        /// </summary>
        public SelectList RoleList { get; set; }

        /// <summary>
        /// Initializes a <see cref="AdmissionViewModel"/> class.
        /// </summary>
        public AdmissionViewModel() { }

        /// <summary>
        /// Initializes a <see cref="AdmissionViewModel"/> class after cast from <see cref="Admission"/> object.
        /// </summary>
        public AdmissionViewModel(Admission item)
        {
            ID = item.ID;
            Description = item.Description;
            Role = item.Role;
        }

        /// <summary>
        /// Overrides default type conversion operator to a user-defined type conversion operator 
        /// that must be invoked with a cast.
        /// </summary>
        /// <param name="item">The <see cref="Admission"/> object</param>
        public static explicit operator AdmissionViewModel(Admission item)
        {
            return new AdmissionViewModel(item);
        }
    }
}