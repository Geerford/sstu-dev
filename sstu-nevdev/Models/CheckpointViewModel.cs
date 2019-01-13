using Domain.Core;
using Service.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    public class CheckpointViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "IP должен быть заполнен")]
        public string IP { get; set; }
        [Required(ErrorMessage = "Корпус должен быть заполнен")]
        public int? Campus { get; set; }
        [Required(ErrorMessage = "Этаж должен быть заполнен")]
        public int? Row { get; set; }
        [Required(ErrorMessage = "Описание должно быть заполнено")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Выберите статус")]
        public string Status { get; set; }
        public SelectList StatusList { get; set; }
        public TypeDTO Type { get; set; }
        [Required(ErrorMessage = "Выберите тип")]
        public string TypeID { get; set; }
        public SelectList TypeList { get; set; }
        public IEnumerable<Admission> Admissions { get; set; }
        public List<CheckboxItem> AdmissionList { get; set; }

        public CheckpointViewModel()
        {
            Admissions = new List<Admission>();
        }

        public CheckpointViewModel(CheckpointDTO item)
        {
            ID = item.ID;
            IP = item.IP;
            Campus = item.Campus;
            Row = item.Row;
            Description = item.Description;
            Status = item.Status;
            Admissions = item.Admissions;
            Type = item.Type;
        }

        public static explicit operator CheckpointViewModel(CheckpointDTO item)
        {
            return new CheckpointViewModel(item);
        }
    }
}