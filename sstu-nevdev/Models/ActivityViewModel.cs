using Domain.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    public class ActivityViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "GUID пользователя должен быть заполнен")]
        public string IdentityGUID { get; set; }
        [Required(ErrorMessage = "IP должен быть заполнен")]
        public string CheckpointIP { get; set; }
        [Required(ErrorMessage = "Выберите дату")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Выберите статус")]
        public string Status { get; set; }
        public SelectList StatusList { get; set; }
        [Required(ErrorMessage = "Выберите режим")]
        [StringLength(20)]
        public string Mode { get; set; }
        public SelectList ModeList { get; set; }

        public ActivityViewModel() { }

        public ActivityViewModel(Activity item)
        {
            ID = item.ID;
            IdentityGUID = item.IdentityGUID;
            CheckpointIP = item.CheckpointIP;
            Date = item.Date;
            Mode = item.Mode;
            Status = item.Status.Equals(true) ? "Успех" : "Неудача";
        }

        public static explicit operator ActivityViewModel(Activity item)
        {
            return new ActivityViewModel(item);
        }
    }
}