using Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sstu_nevdev.Models
{
    public class AdmissionViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Описание должно быть заполнено")]
        [StringLength(100)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Выберите роль")]
        public string Role { get; set; }
        public SelectList RoleList { get; set; }
        
        public AdmissionViewModel() { }

        public AdmissionViewModel(Admission item)
        {
            ID = item.ID;
            Description = item.Description;
            Role = item.Role;
        }

        public static explicit operator AdmissionViewModel(Admission item)
        {
            return new AdmissionViewModel(item);
        }
    }
}