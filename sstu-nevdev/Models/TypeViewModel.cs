using Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents a type model for MVC.
    /// </summary>
    public class TypeViewModel
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
        /// Gets or sets the type status.
        /// </summary>
        [Required(ErrorMessage = "Выберите статус")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SelectList"/> of status.
        /// </summary>
        public SelectList StatusList { get; set; }

        /// <summary>
        /// Initializes a <see cref="TypeViewModel"/> class.
        /// </summary>
        public TypeViewModel() { }

        /// <summary>
        /// Initializes a <see cref="TypeViewModel"/> class after cast from <see cref="Type"/> object.
        /// </summary>
        public TypeViewModel(Type item)
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
        public static explicit operator TypeViewModel(Type item)
        {
            return new TypeViewModel(item);
        }
    }
}