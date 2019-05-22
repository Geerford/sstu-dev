using Domain.Core;
using Service.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents a activity model for MVC.
    /// </summary>
    public class ActivityViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user GUID.
        /// </summary>
        [Required(ErrorMessage = "GUID пользователя должен быть заполнен")]
        public string IdentityGUID { get; set; }

        /// <summary>
        /// Gets or sets the checkpoint IP-address.
        /// </summary>
        [Required(ErrorMessage = "IP должен быть заполнен")]
        public string CheckpointIP { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [Required(ErrorMessage = "Выберите дату")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets or sets the event status.
        /// </summary>
        [Required(ErrorMessage = "Выберите статус")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SelectList"/> of statuses.
        /// </summary>
        public SelectList StatusList { get; set; }

        /// <summary>
        /// Gets or sets the event mode.
        /// </summary>
        [Required(ErrorMessage = "Выберите режим")]
        [StringLength(20)]
        public string Mode { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SelectList"/> of modes.
        /// </summary>
        public SelectList ModeList { get; set; }

        /// <summary>
        /// Initializes a <see cref="ActivityViewModel"/> class.
        /// </summary>
        public ActivityViewModel() { }

        /// <summary>
        /// Initializes a <see cref="ActivityViewModel"/> class after cast from <see cref="Activity"/> object.
        /// </summary>
        public ActivityViewModel(Activity item)
        {
            ID = item.ID;
            IdentityGUID = item.IdentityGUID;
            CheckpointIP = item.CheckpointIP;
            Date = item.Date;
            Mode = item.Mode.Description;
            Status = item.Status.Equals(true) ? "Успех" : "Неудача";
        }

        /// <summary>
        /// Overrides default type conversion operator to a user-defined type conversion operator 
        /// that must be invoked with a cast.
        /// </summary>
        /// <param name="item">The <see cref="Activity"/> object</param>
        public static explicit operator ActivityViewModel(Activity item)
        {
            return new ActivityViewModel(item);
        }
    }

    /// <summary>
    /// Represents a activity details model for MVC.
    /// </summary>
    public class ActivityDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        public Activity Activity { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public IdentityDTO User { get; set; }

        /// <summary>
        /// Gets or sets the checkpoint.
        /// </summary>
        public CheckpointDTO Checkpoint { get; set; }
    }
}