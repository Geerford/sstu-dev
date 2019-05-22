using Domain.Core;
using Service.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    /// <summary>
    /// Represents a checkpoint model for MVC.
    /// </summary>
    public class CheckpointViewModel
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the IP-address.
        /// </summary>
        [Required(ErrorMessage = "IP должен быть заполнен")]
        public string IP { get; set; }

        /// <summary>
        /// Gets or sets the campus.
        /// </summary>
        [Required(ErrorMessage = "Корпус должен быть заполнен")]
        public int? Campus { get; set; }

        /// <summary>
        /// Gets or sets the floor.
        /// </summary>
        [Required(ErrorMessage = "Этаж должен быть заполнен")]
        public int? Row { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        [Required(ErrorMessage = "Секция должна быть заполнена")]
        public int? Section { get; set; }

        /// <summary>
        /// Gets or sets the classroom.
        /// </summary>
        public int? Classroom { get; set; }

        /// <summary>
        /// Gets or sets the desciption.
        /// </summary>
        [Required(ErrorMessage = "Описание должно быть заполнено")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the checkpoint status.
        /// </summary>
        [Required(ErrorMessage = "Выберите статус")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SelectList"/> of status.
        /// </summary>
        public SelectList StatusList { get; set; }

        /// <summary>
        /// Gets or sets the checkpoint type.
        /// </summary>
        public TypeDTO Type { get; set; }

        /// <summary>
        /// Gets or sets the ID of checkpoint type.
        /// </summary>
        [Required(ErrorMessage = "Выберите тип")]
        public string TypeID { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SelectList"/> of type.
        /// </summary>
        public SelectList TypeList { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Admission"/>.
        /// </summary>
        public IEnumerable<Admission> Admissions { get; set; }

        /// <summary>
        /// Gets or sets the admission collection of <see cref="CheckboxItem"/>.
        /// </summary>
        public List<CheckboxItem> AdmissionList { get; set; }

        /// <summary>
        /// Initializes a <see cref="CheckpointViewModel"/> class.
        /// </summary>
        public CheckpointViewModel()
        {
            Admissions = new List<Admission>();
        }

        /// <summary>
        /// Initializes a <see cref="CheckpointViewModel"/> class after cast from <see cref="CheckpointDTO"/> object.
        /// </summary>
        public CheckpointViewModel(CheckpointDTO item)
        {
            ID = item.ID;
            IP = item.IP;
            Campus = item.Campus;
            Row = item.Row;
            Section = item.Section;
            Classroom = item.Classroom;
            Description = item.Description;
            Status = item.Status;
            Admissions = item.Admissions;
            Type = item.Type;
            TypeID = item.Type.ID.ToString();
        }

        /// <summary>
        /// Overrides default type conversion operator to a user-defined type conversion operator 
        /// that must be invoked with a cast.
        /// </summary>
        /// <param name="item">The <see cref="CheckpointDTO"/> object</param>
        public static explicit operator CheckpointViewModel(CheckpointDTO item)
        {
            return new CheckpointViewModel(item);
        }
    }
}